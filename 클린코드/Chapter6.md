# 6장. 객체와 자료 구조

## 자료 추상화

```java
// 6-1. Concrete Point
public class Point {
    public double x;
    public double y;
}
```

```java
// Abstract Point
public interface Point {
    double getX();
    double getY();
    void setCartesian(double x, double y);
    double getR();
    double getTheta();
    void setPolar(double r, double theta);
}
```

- 자동차 연료 상태를 구체적인 숫자 값으로 알려주는 interface

```java
// Concrete Vehicle
public interface Vehicle {
    double getFuelTankCapacityInGallons();
    double getGallonsOfGasoline();
}
```

- 자동차 연료 상태를 백분율이라는 추상적인 개념으로 알려 주는 interface

```java
// Abstact Vehicle
public interface Vehicle {
    double getPercentFuelRemaining();
}
```

- 자료를 세세하게 공개하기보다는 추상적인 개념으로 표현하는 편이 좋다.
- 인터페이스나 조회/설정 함수만으로는 추상화가 이뤄지지 않는다.
- 개발자는 객체가 포함하는 자료를 표현할 가장 좋은 방법을 심각하게 고민해야 한다.
- 아무 생각 없이 조회/설정 함수를 추가하는 방법이 가장 나쁘다.

## 자료/객체 비대칭

## 디미터 법칙

- 디미터 법칙: 클래스 C의 메서드 f는 다음과 같은 객체의 메서드만 호출해야 한다.

  - 클래스 C
  - f가 생성한 객체
  - f 인수로 넘어온 객체
  - C 인스턴스 변수에 저장된 객체

```java
// 디미터 법칙을 위반한 코드
final String outputDir = ctxt.getOptions().getScratchDir().getAbsolutePath();
```

### 기차 충돌

- 위와 같은 코드를 기차 충돌(train wreck)이라고 부른다.
- 위와 같은 코드는 피하라!
- 아래와 같이 나누는 게 좋다.

```java
Options opts = ctxt.getOptions();
File scratchDir = opts.getScratchDir();
final String outputDir = scratchDir.getAbsolutePath();
```

- 디미터 법칙을 거론할 필요가 없는 코드

```java
final String outputDir = ctxt.options.scratchDir.absolutePath;
```

### 잡종 구조

### 구조체 감추기

```java
BufferedOutputStream bos = ctxt.createScratchFileStream(classFileName);
```

## 자료 전달 객체(Data Transfer Object, DTO)

### 활성 레크도

## 결론

- 객체는 동작을 공개하고 자룔를 숨긴다.
  - 기존 동작을 변경하지 않으면서 새 객체 타입을 추가하기는 쉬운 반면, 기존 객체에 새 동작을 추가하기는 어렵다.
- 자료 구조는 별다른 동작 없이 자료를 노출한다.
  - 기존 자료 구조에 새 동작을 추가하기는 쉬우나, 기존 함수에 새 자료 구조를 추가하기는 어렵다.
- 시스템을 구현할 대, 새로운 자료 타입을 추가하는 유연성이 필요하면 객체가 더 적합하다.
- 새로운 동작을 추가하는 유연성이 필요하면 자료 구조와 절차적인 코드가 더 적합하다.
- 우수한 소프트웨어 개발자는 편견 없이 이 사실을 이해해 직면한 문제에 최적인 해결책을 선택한다.
