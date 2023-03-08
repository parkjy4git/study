# 2장. 의미 있는 이름(Meaningful Names)

## 의도를 분명히 밝혀라

변수나 함수 그리고 클래스 이름은 (주석을 필요하지 않을 정도로) 그 의도를 분명히 드러내야 한다.

```text
- (변수나 함수 그리고 클래스의) 존재 이유, 수행 기능, 사용 방법
```

```java
// * 의도가 드러나는 이름

// bad
int d; // 경과 시간(단위: 날짜)

// good - 측정하려는 값과 단위를 표현하는 이름
int elapsedTimeInDays;
int daysSinceCreation;
int daysSinceModification;
int fileAgeInDays;
```

```java
// * 코드 맥락이 코드 자체에 명시적으로 드러나야 한다. (함축적이면 안된다.)

// bad
public List<int[]> getThem() {
    List<int[]> list1 = new ArrayList<int[]>();
    for (int[] x : theList)
        if (x[0] == 4)
            list1.add(x);
    return list1;
}

// good
public List<int[]> getFlaggedCells() {
    List<int[]> flaggedCells = new ArrayList<int[]>();
        for (int[] cell : gameBoard)
            if (cell[STATUS_VALUE] == FLAGGED)
            flaggedCells.add(cell);
    return flaggedCells;
}

// better
/*
* - int[] -> Cell 클래스로 변경
* - 좀 더 명시적인 isFlagged() 함수 사용 
*/ 
public List<Cell> getFlaggedCells() {
    List<Cell> flaggedCells = new ArrayList<Cell>();
        for (Cell cell : gameBoard)
            if (cell.isFlagged())
                flaggedCells.add(cell);
    return flaggedCells;
}
```

## 그릇된 정보를 피하라

- 나름대로 널리 쓰이는 의미가 있는 단어를 다른 의미로 사용해도 안 된다.

```java
// 예
hp, aix, sco 
 
// unix 플랫폼이나 그 변종을 가르키는 이름
// hp는 빗변(hypotenuse)을 구현할 때는 훌륭한 약어지만 독자에겐 그릇된 정보를 제공
```

- 실제 List가 아니라면, List를 붙이지 않는다.

```java
// bad
accountList

// good
accountGroup, bunchOfAccount, account
```

- 서로 흡사한 이름을 사용하지 않는다.

```java
// 예
XYZControllerForEfficientHandlingOfStrings 
XYZControllerForEfficientStorageOfStrings
```

- 유사한 개념은 유사한 표기법을 사용한다. 일관성이 떨어지는 표기법은 그릇된 정보다.

```java
// 이름으로 그릇된 정보를 제공하는 예
소문자 L, 숫자 1
알파벳 O, 숫자 0
```

## 의미 있게 구분하라

- 연속된 숫자를 덧붙인 이름(a1, a2, ..., aN)은 의도적인 이름과 정반대다.

```java
// bad
public static void copyChars(char a1[], char a2[]) {
    for (int i = 0; i < a1.length; i++) {
        a2[i] = a1[i];
    }
}

// good
public static void copyChars(char source[], char destination[]) {
    for (int i = 0; i < source.length; i++) {
        destination[i] = source[i];
    }
}
```

- 불용어(noise word)를 추가한 이름을 사용하지 말아라.

```java
// 의미가 불분명한 불용어 
ProductInfo, ProductData

// 불용어는 중복
// - 변수 이름에 variable 안 된다
// - 표 이름에 table 안 된다
NameString
CustomerObject

```

- 읽는 사람이 차이를 알도록 이름을 지어라.

```java
// 구분이 안 된다.
moneyAmount, money
customerInfo, customer
accountData, account
theMessage, message
```

## 발음하기 쉬운 이름을 사용하라

- 발음하기 어려운 이름은 토론하기도 어렵다.

```java
// bad
class DtaRcrd102 {
    private Date genymdhms;
    private Date modymdhms;
    private final String pszqint = "102";
};

// good
class Customer {
    private Date generationTimestamp;
    private Date modificationTimestamp;;
    private final String recordId = "102";
};
```

## 검색하기 쉬운 이름을 사용하라

- 긴 이름이 짧은 이름보다 좋다. (로컬 변수만 한 문자로 사용)
- **이름 길이는 범위 크기에 비례해야 한다**
- 변수나 상수를 코드 여러 곳에서 사용한다면 검색하기 쉬운 이름이 바람직하다.

```text
MAZ_CLASSES_PER_STUDENT, WORK_DAYS_PER_WEEK 가 7,5 보다는 쉽게 검색
```

## 인코딩을 피하라

### 헝가리식 표현식(Hungarian Notation)

- 헝가리식 표기법이나 기타 인코딩 방식은 방해가 될 뿐이다. 변수, 함수, 클래스 이름이나 타입을 바꾸기가 어려워지며 읽기도 어렵다.

```java
// 타입이 바뀌어도 이름이 바뀌지 않는다!
PhoneNumber phoneString; 
```

### 멤버 변수 접두어

- 멤버 변수에 `m_`이라는 접두어를 붙일 필요 없다.

### 인터페이스 클래스와 구현 클래스

- 인터페이스 이름은 접두어를 붙이지 않는 편이 좋다.

## 자신의 기억력을 자랑하지 마라

- 루프에서 사용하는 경우를 제외하고, 문자 하나만 사용하는 변수를 사용하지 마라.
  - 루프 사용 변수: i, j, k 등 (l은 절대 안 된다!)

- **명료함이 최고**

## 클래스 이름

- 클래스 이름과 객체 이름은 명사, 명사구

## 메서드 이름

- 동사나 동사구가 적합
- 생성자를 중복정의(overload)할 때는 정적 팩토리 메소드를 사용한다. 메서드는 인수를 설명하는 이름을 사용한다.

```java
Complex fulcrumPoint = Complex.FromRealNumber(23.0);
```

- 생성자 사용을 제한하려면 private로 선언한다.

## 기발한 이름은 피하라

- 재미난 이름보다 명료한 이름을 선택하라.
- 특정 문화에서만 사용하는 농담은 피하라.
- 의도를 분명하고 솔직하게 표현하라.

## 한 개념에 한 단어를 사용하라

- 추상적인 개념 하나에 단어 하나를 선택해 고수하라.

```text
예. 같은 메서드를 클래스 마다 fetch, retrieve, get 등으로 각각 사용하면 혼란스럽다.
```

- 메서드 이름은 독자적이고 일관적이어야 한다. (주석을 보지 않고도 올바르게 선택)
- 동일 기반 코드에 controller, mananger, driver를 섞어 사용하지 않는다.

## 말장난을 하지 마라

- 한 단어를 두가지 목적으로 사용하지 마라.

## 해법 영역에서 가져온 이름을 사용하라 (Use Solution Domain Names)

- 코드를 읽을 사람도 프로그래머임을 명심하라. 전산 용어, 알고리즘 이름, 패턴 이름, 수학 용어 등을 사용해도 괜찮다.
- 기술 개념에는 기술 이름이 가장 적합한 선택이다.

```text
VISITOR 패턴에 친숙한 프로그래머는 AccountVistor라는 이름을 쉽게 이해한다.

* VISITOR 패턴
- 실제 로직을 가지고 있는 객체(Visitor)가 로직을 적용할 객체(Element)를 방문하면서 실행하는 패턴이다. 즉, 로직과 구조를 분리하는 패턴
- 개방-폐쇄 원칙을 적용
```

## 문제 영역에서 가져온 이름을 사용하라 (Use Problem Domain Names)

- 적절한 `프로그래머 용어`가 없다면 문제 영역에서 이름을 가져온다. 코드를 보수하는 프로그래머가 분야 전문가에게 의미를 물어 파악할 수 있다.
- 문제 영역 개념과 관련이 깊은 코드라면 문제 영역에서 이름을 가져와야 한다.

## 의미 있는 맥락을 추가하라

- well-named 클래스, 함수, 네임스페이스에 이름을 넣어 맥락을 부여한다.

```java
// 맥락이 불분명한 변수
private void printGuessStatistics(char candidate, int count) {
    String number;
    String verb;
    String pluralModifier;
    
    if (count == 0) {
        number = "no";
        verb = "are";
        pluralModifier = "s";
    } else if (count == 1) {
        number = "1";
        verb = "is";
        pluralModifier = "";
    } else {
        number = Integer.toString(count);
        verb = "are";
        pluralModifier = "s";
    }
    
    String guessMessage = String.format(
      "There %s %s %s%s", verb, number, candidate, pluralModifier
    );
    
    print(guessMessage);
}
```

```java
// 맥락이 분명한 변수
public class GuessStatisticsMessage {

    private String number;
    private String verb;
    private String pluralModifier;

    public String make(char candidate, int count) {
        createPluralDependentMessageParts(count);
        return String.format(
            "There %s %s %s%s",
            verb, number, candidate, pluralModifier );
    }

    private void createPluralDependentMessageParts(int count) {
        if (count == 0) {
            thereAreNoLetters();
        } else if (count == 1) {
            thereIsOneLetter();
        } else {
            thereAreManyLetters(count);
        }
    }

    private void thereAreManyLetters(int count) {
        number = Integer.toString(count);
        verb = "are";
        pluralModifier = "s";
    }

    private void thereIsOneLetter() {
        number = "1";
        verb = "is";
        pluralModifier = "";
    }

    private void thereAreNoLetters() {
        number = "no";
        verb = "are";
        pluralModifier = "s";
    }
}
```

## 불필요한 맥락을 없애라

- Gas Station Deluxe 애플리케이션을 짠다고, 모든 클래스에 GSD로 시작하면 안된다.
- 일반적으로 짧은 이름이 긴 이름보다 좋다. (단, 의미가 분명한 경우)
- 이름에 불필요한 맥락을 추가하지 않도록 주의한다.

```text
 accountAddress, customerAddress는 인스턴스 이름으로는 괜찮지만, 클래스 이름으로는 적합하지 않다.
 Address가 클래스 이름으로 적합
```

## 마치면서

- 좋은 이름을 선택하려면 설명 능력이 뛰어나야 하고 문화적인 배경이 같아야 한다.
- 가독성을 높이려면, 이름을 개선하라!
