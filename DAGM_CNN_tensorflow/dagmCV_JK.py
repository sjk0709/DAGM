#!/usr/bin/env python

import sys, os
sys.path.append(os.pardir)  # parent directory
#import tensorflow as tf
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.animation as animation
import matplotlib.gridspec as gridspec
from sklearn.feature_extraction import image
# from PIL import Image
import cv2
import glob
import random
import struct


# PIL_JK class includes PIL util made by JK

class Data(object):
    def __init__(self):
        self.images = np.zeros(1)
        self.labels = np.zeros(1)
        self.start_batch = 0
        self.end_batch = 0
        self.num_examples = 0
        
    def next_batch(self, batch_size):
        mini_batch = np.random.choice(len(self.images), batch_size, replace=False)
        
#        self.end_batch = self.start_batch+batch_size
#        mini_batch = np.arange(self.start_batch, self.end_batch)
#        if self.end_batch!=len(self.images):
#            self.start_batch = self.end_batch
#        else :
#            self.start_batch = 0
            
        return self.images[mini_batch], self.labels[mini_batch]
              

def genImgListWithFilename(folderpath, imgType, start, end): # input : path  # output : imgList   # path안의 이미지들을 리스트로 만들어준다.
        imgList = []    
        for i in range(start, end+1):
            filepath = folderpath+ '/' + str(i) + '.' + imgType                       
            image = cv2.imread(filepath, cv2.IMREAD_GRAYSCALE)  # B, G, R               
#            cv2.imshow('ddd',image)
#            cv2.waitKey(0)
            imgList.append(image)    
        return imgList    


def cvRotateImg(img, angle):    
    rows = img.shape[0]
    cols = img.shape[1]
    M = cv2.getRotationMatrix2D((cols/2,rows/2),angle,1)
    image = cv2.warpAffine(img,M,(cols,rows))
    return image
   
# data augmentation                  
def dataAugmentation(image):
    Xli = []
    
    verticalFlip = cv2.flip(image,1)         # vertical flip                                 
    for i in range(1, 5):                    
        augmentedImg1 = cvRotateImg(image, 90*i)
        augmentedImg2 = cvRotateImg(verticalFlip, 90*i)
        Xli.append(augmentedImg1)        
        Xli.append(augmentedImg2)        
        
    return Xli
 
class DAGM(object):
    """
    
    """
    def __init__(self, dataPath):
        self.dataPath = dataPath
        
        self._blockW = 32
        self._blockH = 32        
        self.nBlockPerImage = 5
        self.nOKimgPerClass = 80
        self.nNGimgPerClass = 80
        
        self.trainOKImgIndices = []
        self.testOKImgIndices = []
        self.trainNGImgIndices = []        
        self.testNGImgIndices = []
        for i in range(1, 701): self.trainOKImgIndices.append(i)
        for i in range(701, 1001): self.testOKImgIndices.append(i)
        for i in range(1, 106): self.trainNGImgIndices.append(i)        
        for i in range(106, 151): self.testNGImgIndices.append(i)
        
        #readFreeImg()
        self.train = Data()
        self.test = Data()      
        
      
    def extractBlocksInOK(self, classNo, isTrain=True ):
       
        tempXli = []
        tempYli = []                    

        chosen_indices = ''
        
        classPath = self.dataPath + '/Class' + str(classNo) + '/'
        Y = np.zeros([12], dtype='float32')
        Y[classNo-1] = 1.

        if isTrain:
            if self.nOKimgPerClass>len(self.trainOKImgIndices):
                self.nOKimgPerClass=len(self.trainOKImgIndices)
            chosen_indices = random.sample(self.trainOKImgIndices, self.nOKimgPerClass)     
        else:
            if self.nOKimgPerClass>len(self.testOKImgIndices):
                self.nOKimgPerClass=len(self.testOKImgIndices)
            chosen_indices = random.sample(self.testOKImgIndices, self.nOKimgPerClass)
        for imgNo in chosen_indices:
            Xpath = classPath + str(imgNo) + '.png'
            tempX = cv2.imread(Xpath, cv2.IMREAD_GRAYSCALE)  # B, G, R  
            tempX = image.extract_patches_2d(tempX, (self._blockW, self._blockH), max_patches=self.nBlockPerImage)  
            
            for j in range(self.nBlockPerImage):
                tempXli.append(tempX[j]/255.)
                tempYli.append(Y)               
  
        return tempXli, tempYli 
    
    def extractBlocksInNG(self, classNo, labelInfo, isTrain=True ):
       
        tempXli = []
        tempYli = []                    

        chosen_indices = ''
          
        classPath = self.dataPath + '/Class' + str(classNo) + '_def/'     
        Y = np.zeros([12], dtype='float32')
        Y[classNo+5] = 1.
                
        if self.nNGimgPerClass<8:
            self.nNGimgPerClass=8   
        
        if isTrain:
            numNGimgPerClass = int(self.nNGimgPerClass/8.)   
            if numNGimgPerClass>len(self.trainNGImgIndices):
                numNGimgPerClass=len(self.trainNGImgIndices)                     
            chosen_indices = random.sample(self.trainNGImgIndices, numNGimgPerClass)  
            
        else:  
            numNGimgPerClass = int(self.nNGimgPerClass/8.)
            if numNGimgPerClass>len(self.testNGImgIndices):
                numNGimgPerClass=len(self.testNGImgIndices)            
            chosen_indices = random.sample(self.testNGImgIndices, numNGimgPerClass)

        for imgNo in chosen_indices:
            
            Xpath = classPath + str(imgNo) + '.png'
            tempX = cv2.imread(Xpath, cv2.IMREAD_GRAYSCALE)  # B, G, R     
            lastX = tempX.shape[1]-self._blockW-1
            lastY = tempX.shape[0]-self._blockH-1
            
            tempInfo = labelInfo[imgNo-1]
            semi_major = float(tempInfo[1])  # semi-major axis
            semi_minor = float(tempInfo[2])  # semi-minor axis    
            rotAngle = float(tempInfo[3])       # rotation angle
            cx = float(tempInfo[4])  # x of the centre
            cy = float(tempInfo[5])  # y of the centre
            
            for j in range(self.nBlockPerImage):
                
                theta =  random.uniform(0, 2*np.pi)
                x = random.uniform(0, semi_major)*np.cos(theta)
                y = random.uniform(0, semi_minor)*np.sin(theta)
                xp = x*np.cos(rotAngle) - y*np.sin(rotAngle)
                yp = x*np.sin(rotAngle) + y*np.cos(rotAngle)
                xp = int(cx + xp)
                yp = int(cy + yp)
                
                left = xp-int(0.5*self._blockW)
                top = yp-int(0.5*self._blockH)
                
                if(left<0 ):
                    left = 0
                if(left>lastX):
                    left = lastX
                if(top<0):
                    top = 0
                if(top>lastY ):
                    top = lastY   
                    
                start_x = left
                end_x = left+self._blockW
                start_y = top
                end_y = top +self._blockH    
                
                cropX = tempX[start_y:end_y, start_x:end_x]
                
                # data augmentation 
                tempXli += dataAugmentation(cropX/255.)
                for k in range(8): tempYli.append(Y)
                
                # data augmentation                         
#                for i in range(1, 5):       
#                    for j in [True, False]:
#                        augmentedImg = cvRotateImg(original, 90*i)
#                        if j:
#                            augmentedImg = cv2.flip(augmentedImg,0)     # horizontal flip
#                        Xli.append(augmentedImg/255.)
#                        Yli.append(Y)
                
           
#        print("the number of images without defect : ", len(tempXli)) # the number of defective free images
        
        return tempXli, tempYli 
    
    def genBlockData(self, blockW, blockH, nOKimgPerClass=160, nNGimgPerClass=160, isTrain=True):

        self._blockW = blockW
        self._blockH = blockH        
        self.nOKimgPerClass = nOKimgPerClass
        self.nNGimgPerClass = nNGimgPerClass
        
        dataX = []   
        dataY = []
        
        for classNo in range(1, 7):
            
            # OK
            bufferX, bufferY = self.extractBlocksInOK(classNo, isTrain=isTrain)
            dataX += bufferX       # training set X
            dataY += bufferY       # training set Y  
            
            # NG
            labelInfoPath = self.dataPath + '/Class' + str(classNo) + '_def/'           
            labelInfoFile = open(labelInfoPath+'labels.txt', 'r')        
            labelInfoList = []
            for i in range(150):                
                labelInfoList.append( labelInfoFile.readline().split()  )
                    
            bufferX, bufferY = self.extractBlocksInNG(classNo, labelInfoList, isTrain=isTrain)
            dataX += bufferX       # training set X
            dataY += bufferY       # training set Y  
                 
        dataX = np.array(dataX, dtype=np.float32)
        dataY = np.array(dataY, dtype=np.float32)
        
        if isTrain:
            self.train.images = dataX
            self.train.images = self.train.images.reshape(-1, blockW*blockH*1)
            self.train.labels = dataY
            self.train.num_examples = self.train.images.shape[0]
            print("Train images shape :", self.train.images.shape)
            print("Train labels shape :", self.train.labels.shape)
        else :
            self.test.images = dataX
            self.test.images = self.test.images.reshape(-1, blockW*blockH*1)
            self.test.labels = dataY
            self.test.num_examples = self.test.images.shape[0]
            print("Test images shape :", self.test.images.shape)
            print("Test labels shape :", self.test.labels.shape)
 
 
    
    
   

  
    
if __name__ == '__main__':  
    

    dataPath = '../DAGM2007_dataset'

        
    dagm = DAGM(dataPath)  
    
    batch_size = 10
        
    for i in range(10):
        trainX, trainY = dagm.train.next_batch(batch_size)
        print(trainX.shape)
        #print(trainX)
        print(trainY.shape)
        #print(trainY)
        print('-----------------------------------------------------')

    
    images = dagm.train.images
    labels = dagm.train.labels
    print(images.shape)
    print(labels.shape)
    
#    dagm.genImgWithoutDefect()

#    dagm.genImgArr()
#    dagm.genImgArrWithoutDefect()
#    dagm.genImgArrWithDefect()
#
#

#    dagm.readImgWithoutDefect(1)
#    trainX = dagm.train.images
#    trainY = dagm.train.labels
#    print(trainX[0])
#    print(trainY[0])
#    trX, trY = dagm.train.next_batch(5)
#    print(dagm.train.num_examples)
#    print( trX.shape, trY.shape)
#    X = trainX[0].reshape(32,32)
#    plt.imshow(np.array(X), cmap=plt.get_cmap('gray'))   
#    plt.show()
# 
