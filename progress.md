
## Defect inspection by CNN
### general CNN


## Defect inspection by FCN
### FCN with resnet152
* feature size가 작으므로 첫 번째 layer인
nn.Conv2d(1, 64, kernel_size=7, stride=2, padding=3, bias=False) 
의 kernel_size와 padding을 수정하였다.
=> nn.Conv2d(1, 64, kernel_size=3, stride=2, padding=1, bias=False) 

* 마찬가지로 MaxPooling도 수정
nn.MaxPool2d(kernel_size=3, stride=2, padding=1) 
=> nn.MaxPool2d(kernel_size=2, stride=2)
