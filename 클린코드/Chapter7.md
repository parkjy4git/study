# 7장. 오류 처리

- 오류 처리는 프로그램에 반드시 필요한 요소 중 하나이다.
- 깨끗한 코드와 오류 처리는 확실히 연관성이 있다.
- 이 장에서는 오류를 처리하는 기법과 고려 사항 몇 가지를 소개한다.

## 오류 코드보다 예외를 사용하라

- 얼마 전까지만 해도 예외를 지원하니 않는 프로그래밍 언어가 많았다.
  - 예외를 지원하지 않는 언어는 오류를 처리하고 보고하는 방법이 제한적이었다.
  - 플래그를 설정하거나 호출자에게 오류 코드를 반환하는 방법이 전부였다.

```java
// 오류 코드를 반환하는 방법을 사용
public class DeviceController {
...
    public void sendShutDown() {
        DeviceHandle handle = getHandle(DEV1);
        // Check the state of the device
        if (handle != DeviceHandle.INVALID) {
            // Save the device status to the record field
            retrieveDeviceRecord(handle);
            // If not suspended, shut down
            if (record.getStatus() != DEVICE_SUSPENDED) {
                pauseDevice(handle);
                clearDeviceWorkQueue(handle);
                closeDevice(handle);
            } else {
                logger.log("Device suspended. Unable to shut down");
            }
            } else {
                logger.log("Invalid handle for: " + DEV1.toString());
            }
        }
        ...
    }

```

- 오류 코드 사용
  - 함수를 호출한 즉시 오류를 확인해야 하기 때문에 호출자 코드가 복잡

- 예외를 사용
  - 호출자 코드가 더 깔끔해진다. (로직이 오류 처리 코드와 뒤섞이지 않으니깐)

```java
// 예외를 사용
public class DeviceController {
    ...
    public void sendShutDown() {
        try {
            tryToShutDown();
        } catch (DeviceShutDownError e) {
            logger.log(e);
        }
    }   
    
    private void tryToShutDown() throws DeviceShutDownError {
        DeviceHandle handle = getHandle(DEV1);
        DeviceRecord record = retrieveDeviceRecord(handle);
        pauseDevice(handle);
        clearDeviceWorkQueue(handle);
        closeDevice(handle);
    }
        
    private DeviceHandle getHandle(DeviceID id) {
        ...
        throw new DeviceShutDownError("Invalid handle for: " + id.toString());
        ...
    }
    ...
}

```

- 예외를 사용한 예시 코드
  - 디바이스를 종료하는 알고리즘과 오류 처리하는 알고리즘을 분리하여 뒤섞인 개념을 분리
  - 훨씬 더 깨끗해졌다!

## Try-Catch-Finally 문부터 작성하라

## 미확인(Unchecked) 예외를 사용하라

> Checked Exception - 소프트웨어 라이브러리를 의도대로 사용하더라도 발생할 수 있는 예측 가능한 오류 상황을 말한다. 예. FileNotFoundException  
> Unchecked Exception - 런타임에 발생하여 컴파일러가 예상하지 못하는 오류  

- Checked Exception
  - 몇 가지 장점을 제공하지만, 지금은 안정적인 소프트웨어를 제작하는 요소로 확인된 예외가 반드시 필요하지는 않다는 사실이 분명하다.
  - OCP를 위반한다.

## 예외에 의미를 제공하라

## 호출자를 고려해 예외 클래스를 정의하라

- 애플리케이션에서 오류를 정의할 때 프로그래머에게 가장 중요한 관심사는 **오류를 잡아내는 방법**이 되어야 한다.

- Wrapper Class는 매우 유용하다
  - 외부 API를 사용할 때는 Wrapping이 최선이다.
  - 외부 라이브러리와 프로그램 사이에서 의존성이 크게 줄어든다.
  - 다른 라이브러리로 변경하는 비용도 적다.
  - 프로그램 테스트도 쉬워진다.

## 정상 흐름을 정의하라

## null을 반환하지 마라

## null을 전달하지 마라

## 결론

- 깨끗한 코드는 읽기도 좋아야 하지만 안정성도 높아야 한다.
- 오류 처리를 프로그램 논리와 분리해 독자적인 사안으로 고려하면 튼튼하고 깨끗한 코드를 작성할 수 있다.
- 오류 처리를 프로그램 논리와 분리하면 독립적인 추론이 가능해지며 코드 유지보수성도 크게 높아진다.
