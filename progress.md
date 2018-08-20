
## 1.Defect inspection by CNN with original label data
---
#### resnetJK0.py
* feature size가 작으므로 첫 번째 layer인
nn.Conv2d(1, 64, kernel_size=7, stride=2, padding=3, bias=False) 
의 kernel_size와 padding을 수정하였다.
=> nn.Conv2d(1, 64, kernel_size=3, stride=2, padding=1, bias=False) 

* 마찬가지로 MaxPooling도 수정
nn.MaxPool2d(kernel_size=3, stride=2, padding=1) 
=> nn.MaxPool2d(kernel_size=2, stride=2)

##### DAGM2_resnetJK0_resnet152v1_32_12

###### Result
* Accuracy : 95~98%
* optimal1 이 현재 가장 좋은 성능을 보여주는것으로 나타남



---
#### cnn0.py
```python
self.maxPool = nn.MaxPool2d(kernel_size=2, stride=2)  

self.layer1 = self._make_layer(block, 16*k, layers[0], stride=1)
self.layer2 = self._make_layer(block, 32*k, layers[1], stride=1)
self.layer3 = self._make_layer(block, 64*k, layers[2], stride=1)

h = int(input_size[1]/8)
w = int(input_size[2]/8)
self.fc1 = nn.Linear(64*k*h*w, 1024, bias=False)
self.bn1 = nn.BatchNorm2d(1024)
self.relu = nn.ReLU(inplace=True)

self.fc2 = nn.Linear(1024, 1024, bias=False)
self.bn2 = nn.BatchNorm2d(1024)

self.fc3 = nn.Linear(1024, num_classes)
```
##### DAGM2_cnn0_Conv12k4_32_12
conv(1, 64)
conv(64, 64)

conv(64, 128)
conv(128, 128)
conv(128, 128)

conv(128, 256)
conv(256, 256)
conv(256, 256)
conv(256, 256)

fc((in/8)*(in/8)*256, 1024) 
fc(1024, 1024) 
fc(1024, 12)

###### Result
* Accuracy : 96~99% <- Very Good!!!!!!!!!!!!!!!!
* optimal3 이 현재 가장 좋은 성능을 보여주는것으로 나타남


##### DAGM2_cnn0_Conv12k8_32_12
conv(1, 128)
conv(128, 128)

conv(128, 256)
conv(256, 256)
conv(256, 256)

conv(256, 512)
conv(512, 512)
conv(512, 512)
conv(512, 512)

fc((in/8)*(in/8)*512, 1024) 
fc(1024, 1024) 
fc(1024, 12)

###### Result
* Accuracy : 96~99% <- Very Good!!!!!!!!!!!!!!!!
* optimal3 이 현재 가장 좋은 성능을 보여주는것으로 나타남



###### Result
* Accuracy : 96~99% <- Very Good!!!!!!!!!!!!!!!!
* optimal3 이 현재 가장 좋은 성능을 보여주는것으로 나타남

---
---
## 2.Defect inspection by FCN with original label data
#### DAGM2_fcn0_c10d10_128_SB
###### Result
* 결과 좋음


---
---
## 3.Defect inspection by FCN with new label data
#### DAGM3_fcn0_c10d10_128_SB
###### Result
* 결과 매우 좋음
* optimal1 이 현재 가장 좋은 성능을 보여주는것으로 나타남


코드는 나중에~~
