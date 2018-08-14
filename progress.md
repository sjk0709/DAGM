
## Defect inspection by CNN with original label data
#### DAGM2_resnetJK0_resnet152v1_32_12
###### resnetJK0
* feature size가 작으므로 첫 번째 layer인
nn.Conv2d(1, 64, kernel_size=7, stride=2, padding=3, bias=False) 
의 kernel_size와 padding을 수정하였다.
=> nn.Conv2d(1, 64, kernel_size=3, stride=2, padding=1, bias=False) 

* 마찬가지로 MaxPooling도 수정
nn.MaxPool2d(kernel_size=3, stride=2, padding=1) 
=> nn.MaxPool2d(kernel_size=2, stride=2)

###### Result
* Accuracy : 95~98%
* optimal1 model이 현재 가장 좋은 성능을 보여주는것으로 나타남


## Defect inspection by FCN with original label data
#### DAGM2_fcn0_c10d10_128_SB
###### Result
* 결과 좋음



## Defect inspection by FCN with new label data
#### DAGM3_fcn0_c10d10_128_SB
###### Result
* 결과 매우 좋음
* optimal1 model이 현재 가장 좋은 성능을 보여주는것으로 나타남


코드는 나중에~~
