
## 1. Defect inspection by CNN with original label data

#### DAGM2_resnetJK0_resnet34v1_32_12
###### Structure

###### Result
* Accuracy : 97~98%
* optimal0 이 현재 가장 좋은 성능을 보여주는것으로 나타남

#### DAGM2_resnetJK0_resnet50v1_32_12
###### Structure

###### Result
* Accuracy : 97~98%
* optimal0 이 현재 가장 좋은 성능을 보여주는것으로 나타남

#### DAGM2_resnetJK0_resnet152v1_32_12
###### Structure

###### Result
* Accuracy : 95~98%
* optimal1 이 현재 가장 좋은 성능을 보여주는것으로 나타남

#### DAGM2_cnn0_Conv12k4_32_12
###### Structure
* conv(1, 64) - 2 layer
* conv(64, 128) - 3 layer
* conv(128, 256) - 4 layer

* fc((in/8)*(in/8)*256, 1024) 
* fc(1024, 1024) 
* fc(1024, 12)

###### Result
* Accuracy : 96~99% <- Very Good!!!!!!!!!!!!!!!!
* optimal3 이 현재 가장 좋은 성능을 보여주는것으로 나타남


#### DAGM2_cnn0_Conv12k8_32_12
###### Structure
* conv(1, 128) - 2 layer

* conv(128, 256) - 3 layer

* conv(256, 512) - 4 layer

* fc((in/8)*(in/8)*512, 1024) 
* fc(1024, 1024) 
* fc(1024, 12)

###### Result
* Accuracy : 97~98% <- Very Good!!!!!!!!!!!!!!!!
* optimal3 이 현재 가장 좋은 성능을 보여주는것으로 나타남


#### Conclusion
* Resnet : resnet34v1, resnet50v1 등 layer가 더 낮을 때 좋은 성능을 보여줌
* Conv12k : Conv12k4와 Conv12k8 에서 둘 다 비슷하나 Conv12k4가 역시 더 안정적인 성능을 보여줌
```
|==================================================================|
|===== models/DAGM2_resnetJK0_resnet18v1_32_12/recent_all.pkl =====|
|===== Accuracy : 98.1 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_resnetJK0_resnet34v1_32_12/recent_all.pkl =====|
|===== Accuracy : 98.0 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_resnetJK0_resnet50v1_32_12/recent_all.pkl =====|
|===== Accuracy : 97.8 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_resnetJK0_resnet101v1_32_12/recent_all.pkl ====|
|===== Accuracy : 97.5 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_resnetJK0_resnet152v1_32_12/recent_all.pkl ====|
|===== Accuracy : 97.6 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_cnn0_Conv12k4_32_12/recent_all.pkl ============|
|===== Accuracy : 98.4 % ==========================================|
|==================================================================|
|==================================================================|
|===== models/DAGM2_cnn0_Conv12k8_32_12/recent_all.pkl ============|
|===== Accuracy : 98.3 % ==========================================|
|==================================================================|
```

---
## 2. Defect inspection by FCN with original label data
#### DAGM2_fcn0_c10d10_128_SB
###### Structure
###### Result
* 결과 좋음


---
## 3. Defect inspection by FCN with new label data
#### DAGM3_fcn0_c10d10_128_SB
###### Structure
###### Result
* 결과 매우 좋음
* optimal1 이 현재 가장 좋은 성능을 보여주는것으로 나타남

## 4. Defect inspection by FCN
* DAGM2_fcn0_c10d10_128_XX 으로 새로운 Label 생성   => Label_A1
* Label_A1 을 이용하여 Model 생성                   => DAGM_A1_fcn0_c10d10_128_XX
* DAGM_A1_fcn0_c10d10_128_XX 으로 새로운 Label 생성 => Label_A2
* Label_A2 을 이용하여 Model 생성                   => DAGM_A2_fcn0_c10d10_128_XX


###### Result




* 결과 매우 좋음
* optimal1 이 현재 가장 좋은 성능을 보여주는것으로 나타남

코드는 나중에~~

## Network file 설명
#### resnetJK0.py
* feature size가 작으므로 첫 번째 layer인
nn.Conv2d(1, 64, kernel_size=7, stride=2, padding=3, bias=False) 
의 kernel_size와 padding을 수정하였다.
=> nn.Conv2d(1, 64, kernel_size=3, stride=2, padding=1, bias=False) 

* 마찬가지로 MaxPooling도 수정
nn.MaxPool2d(kernel_size=3, stride=2, padding=1) 
=> nn.MaxPool2d(kernel_size=2, stride=2)

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
