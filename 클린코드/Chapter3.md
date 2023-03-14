# 3장. 함수

```text
- 의도를 분명히 표현하는 함수를 어떻게 구현할 수 있을까?
- 함수에 어떤 속성을 부여해야 처음 읽는 사람이 프르그램 내부를 직관적으로 파악할 수 있을까?
```

## 작게 만들어라

- `작게!` `더 작게!` 만들어라.

```java
// 목록 3-3 HtmlUtil.java (re-refactored)
public static String renderPageWithSetupsAndTeardowns(PageData pageData, boolean isSuite) throws Exception {
    if (isTestPage(pageData))
        includeSetupAndTeardownPages(pageData, isSuite);
    return pageData.getHtml();
}
```

### 블록과 들여쓰기

- if, else, while 문 등에 들어가는 블록은 한 줄 이어야 한다.
  - 블록 안에서 호출하는 함수 이름을 적절히 짓는다면, 이해하기도 쉽다.
  - 중첩 구조가 생길만큼 함수가 커져서는 안 된다는 뜻
  - 이렇게 하면, 함수를 읽고 이해하기 쉬워진다.

## 한 가지만 해라

> 함수는 한 가지를 해야 한다. 그 한가지를 잘 해야 한다. 그 한가지만을 해야 한다.

목록 3-3 코드는 간단한 TO 문단으로 기술할 수 있다.

```text
TO RenderPageWithSetupsAndTeardowns, 페이지가 테스트 페이지인지 확인한 후 테스트 페이지라면 설정 페이지와 해제 페이지를 넣는다.
테스트 페이지든 아니든 페이지를 HTML로 랜더링한다.

* TO 
 - LOGO 언어에서 사용하는 키워드로 루비나 파이썬에서 사용하는 `def`와 똑같다. LOGO에서 모든 함수는 키워드 `TO`로 시작한다.

* LOGO
 - 함수형 프로그래밍을 이용하는 교육용 컴퓨터 프로그래밍 언어

```

- 함수가 `한 가지`만 하는지 판단하는 방법
  - 지정된 함수 이름 아래에서 추상화 수준이 하나인 단계만 수행한다면 그 함수는 한 가지 작업만 한다.
  - 의미 있는 이름으로 다른 함수를 추출할 수 있다면 그 함수는 여러 작업을 하는 셈이다.

### 함수 내 섹션

- 한 가지 작업만 하는 함수는 자연스럽게 섹션으로 나누기 어렵다. (예제 코드. 90쪽 목록 4-7)

## 함수 당 추상화 수준은 하나로

- 함수가 `한 가지` 작업만 하려면 함수 내 모든 문장의 추상화 수준이 동일해야 한다.
- 한 함수 내에 추상화 수준을 섞으면 코드를 읽는 사람이 헷갈린다. 특정 표현이 근본 개념인지 아니면 세부사항인지 구분하기 어려운 탓이다.
- 근본 개념과 세부사항이 뒤섞기 시작하면, 깨어진 창문처럼 사람들이 함수에 세부사항을 점점 더 추가한다..

### 위에서 아래로 코드 읽기: *내려가기 규칙(The Stepdown Rule)*

- 코드는 위에서 아래로 이야기처럼 읽혀야 좋다.
  - 한 함수 다음에는 추상화 수준이 한 단계 낮은 함수가 온다.
  - 즉, 위에서 아래로 프로그램을 읽으면 함수 추상화 수진이 한 번에 한 단계씩 낮아진다.
  - 이것을 `내려기기 규칙(The Stepdown Rule)`이라 부른다.

- 다르게 표현하면, 일련의 TO 문단을 읽듯이 프로그램이 읽혀야 한다는 의미다.
  - TO 문단 - 현재 추상화 수준을 설명

```text
- TO 설정 페이지와 해제 페이지를 포함하려면, 설정 페이지를 포함하고, 테스트 페이지 내용을 포함하고, 해제 페이지를 포함한다.
  - TO 설정 페이지를 포함하려면, 슈트이면 슈트 설정 페이지를 포함한 후 일반 설정 페이지를 포함한다.
  - TO 슈트 설정 페이지를 포함하려면, 부모 계층에서 "SuiteSetUp"페이지를 찾아 include 문과 페이지 경로를 추가한다.
  - TO 부모 계층을 검색하려면, ......
```

## Switch 문

- switch 문은 작게 만들기 어렵다. (if-else가 여럿이 이어지는 구문도 마찬가지)
- switch 문을 저차원 클래스에 숨기고 절대로 반복하지 않는 방법 -> 다형성(polymorphism)을 이용

```java
// bad
public Money calculatePay(Employee e) throws InvalidEmployeeType {
    switch (e.type) {
        case COMMISSIONED:
            return calculateCommissionedPay(e);
        case HOURLY:
            return calculateHourlyPay(e);
        case SALARIED:
            return calculateSalariedPay(e);
        default:
            throw new InvalidEmployeeType(e.type);
    }
}
```

- 문제점
  - 함수가 길다. (새 직원 유형을 추가하면 더 길어 진다)
  - `한 가지`작업만 수행하지 않는다.
  - SRP를 위반한다. (코드를 변경할 이유가 여러 가지)
  - OCP를 위반한다. (새 직원 유형을 추가할 때마다 코드를 변경해야 한다.)
  - 위 함수와 동일한 함수가 무한정 존재 한다

```java
// 위 함수와 마찬가지로 switch문을 사용해야 한다.
isPayday(Employee e, Date date);
deliverPay(Employee e, Money pay);
```

- 해결
  - switch 문을 ABSTRACT FACTORY에 숨긴다.
  - FACTORY는 switch 문을 사용해 적절한 Employee 파생 클래스의 인스턴스를 생성
  - calculatePay, isPayday, deliverPay 등의 함수는 Employee 인터페이스를 거쳐 호출한다. (다형성으로 실제 파생 클래스의 함수가 호출 됨)

```java
// good
public abstract class Employee {
    public abstract boolean isPayday();
    public abstract Money calculatePay();
    public abstract void deliverPay(Money pay);
}

public interface EmployeeFactory {
    public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType;
}

public class EmployeeFactoryImpl implements EmployeeFactory {
    public Employee makeEmployee(EmployeeRecord r) throws InvalidEmployeeType {
        switch (r.type) {
            case COMMISSIONED:
                return new CommissionedEmployee(r) ;
            case HOURLY:
                return new HourlyEmployee(r);
            case SALARIED:
                return new SalariedEmploye(r);
            default:
                throw new InvalidEmployeeType(r.type);
        }
    }
}
```

- switch문은 상속 관계로 숨긴 후 절대로 다른 코드에 노출하지 않는다.

## 서술적인 이름을 사용하라

[워드](Chapter1.md#워드-커닝행위키-창시자)
> 코드를 읽으면서 짐작했던 기능을 각 루틴이 그대로 수행한다면 깨끗한 코드라 불러도 되겠다.  

- 함수가 작고 단순할수록 서술적인 이름을 고르기 쉬워진다.
- 이름이 길어도 괜찮다. 길고 서술적인 이름이 짧고 어려운 이름보다 좋다.
- 이름을 정하는데 시간을 들여도 괜찮다. (IDE에서 이름 바꾸기 쉽다.)
- 이름을 붙일 때는 일관성이 있어야 한다. 모듈 내에서 함수 이름은 같은 문구, 명사, 동사를 사용한다.

```text
- includeSetupAndTeardownPages
- includeSetupPages
- includeSuiteSetupPage
- includeSetupPage

** 아래 함수들도 있음을 짐작할 수 있다.
- includeTeardownPages, includeSuiteTeardownPage, includeTeardownPage
```

## 함수 인수(Function Arguments)

- 이상적인 인수 개수는 0개, 그 다음이 1개, 다음은 2개
  - 3개는 피하는 것이 좋다.
  - 4개 이상은 특별한 이유가 있어야 한다. (이유가 있어도 사용하면 안 된다.)
- 인수는 어렵다. 인수는 개념을 이해하기 어렵게 만든다.
- 테스트 관점에서 보면 인수는 더 어렵다.
  - 인수가 많으면 인수마다 유효한 값으로 모든 조합을 구성해 테스트해야한다.
- 출력 인수는 입력 인수보다 이해하기 어렵다.

### 가장 많이 쓰이는 단항 인수

함수에 인수 1개를 사용하는 경우  

- 인수에 질문을 던지는 경우

```java
  boolean fileExists("MyFile");
```

- 인수를 뭔가로 변환해 결과를 반환하는 경우

```java
  InputStream fileOpen("MyFile"); // String형의 파일이름을 InputStream으로 반환
```

- (다소 드물게 사용하지만 그래도 유용한) 이벤트
  - 이벤트 함수는 입력 인수만 있다. 출력 인수는 없다.

  ```java
    passwordAttemptFiledNtimes(int attempts)
  ```
  
  - 이벤트 함수는 조심해서 사용해야 한다.
  - 이벤트라는 사실이 코드에 명확이 드러나야 한다.
  - 이름과 문맥을 주의해서 선택한다.

- 위 세가지가 아니라면, 단항 함수는 가급적 피한다.

```java
  // 예: 변환 함수에서 출력 인수를 사용하면 혼란을 일으킨다
  void includeSetupPageInto(StringBuffer pageText) 
```

### 플래그 인수

- 플래그 인수는 추하다
- 함수로 boolean을 넘기는 관례는 끔찍하다. 함수가 한꺼번에 여러 가지를 처리한다고 대놓고 공표하는 셈이니까!

### 이항 함수

- 인수가 2개인 함수는 인수가 1개인 함수보다 이해하기 어렵다.
- 이항 함수가 적절한 경우

```java
  // 직교 좌표계 점은 인수 2개를 취한다.
  // 여기서 인수 2개는 한 값을 표현하는 두 요소다. 두 요소에는 자연적인 순서도 있다.
  Point p = new Point(0, 0); 
```

- assertEquals(expected, actual)
  - 당연하게 여겨지는 함수지만 문제가 있다. (두 인수를 바꿔서 사용하는 경우가 많다.)
  - expected 다음에 actual이 온다는 사실을 기억해야 한다.

- 이항 함수가 무조건 나쁘건 아니지만, 가능하면 단항 함수로 바꾸도록 해야 한다.

  ```java
   // bad
   writeField(outputStream, name)
  ```

  - writeField 메서드를 outputStream 클래스의 멤버로 만들어라.

  ```java
    // good
    outputStream.writeField(name);
  ```  

  - outputStream을 멤버 변수로 만들어서 사용

  ```java

  private OutputStream outputStream;
  
  // good
  public void writeField(String name) {
    // 멤버 변수 outputStream 사용
  }
  ```

  - 새 클래스(FileWriter)를 만들어 생성자에서 outputStream을 받고 write 메서드를 구현한다.

  ```java
  public class FileWriter {
    
    private final OutputStream outputStream;
    
    FileWriter(OutputStream outputStream) {
      this.outputStream = outputStream;
    }

    // good
    public void write(String name) {
      // outputStream 사용
    }
  }
  ```

### 삼항 함수

- 인수가 3개인 함수는 인수가 2개인 함수보다 훨씬 더 이해하기 어렵다.
  - 순서, 주춤, 무시로 야기되는 문제가 두 배 이상 늘어난다.
  - 삼항 함수를 만들때는 신중히 고려하라.

```java
  assertEquals(message, expected, actual)

  // 첫 인수가 expected라 예상
  // 매번 함수를 볼 때마다 주춤했다가 message를 무시해야 한다고 상기한다.
```

### 인수 객체

- 인수가 2-3개 필요하다면 일부를 독자적인 클래스 변수로 선언할 가능성을 짚어본다.

```java
  Circle makeCircle(double x, double y, double radius);
  
  // 객체를 생성해 인수를 줄이는 방법이 눈속임이 아니라, (변수를 묶어 넘기려면 이름을 붙여야하므로) 개념을 표현 
  Circle makeCircle(Point center, double radius);
```

### 인수 목록

- 때로는 인수 개수가 가변적인 함수도 필요하다. 예. String.Format

```java
  String.format("%s worked %.2f hours.", name, hours);
```

- 가변 인수 전부를 동등하게 취급하면 List 형 인수 하나로 취급할 수 있다.

```java
  // 사실상 이항 함수
  public String format(String format, Object... args);
```

### 동사와 키워드

- 함수의 의도나 인수의 순서와 의도를 제대로 표현하려면 좋은 함수 이름이 필수다.
- 단항 함수는 함수와 인수가 동사/명사 쌍을 이뤄야 한다.

```java
  // good - name이 무엇이든 쓴다(write).
  write(name)
  // better - name이 field라는 사실이 분명히 드러남
  writeField(name)
```

- 함수 이름에 키워드를 추가

```java
  assertEquals(expected, actual)
  
  // better - 순서를 기억할 필요가 없다.
  assertExpectedEqualsActual(expected, actual)
```

## 부수 효과(Side Effects)를 일으키지 마라

- 부수 효과는 거짓말이다. 함수에서 한 가지를 하겠다고 약속하고선 남몰래 다른 짓도 하니까.
- 많은 경우 시간적인 결합(temporal coupling)이나 순서 종속성(order dependency)을 초래한다.

```java
// 목록 3-6
public class UserValidator {
  private Cryptographer cryptographer;
  
  public boolean checkPassword(String userName, String password) {
    User user = UserGateway.findByName(userName);
    if (user != User.NULL) {
      String codedPhrase = user.getPhraseEncodedByPassword();
        String phrase = cryptographer.decrypt(codedPhrase, password);
      if ("Valid Password".equals(phrase)) {
        Session.initialize(); // 세션을 초기화 한다는 사실이 함수 이름에 드러나지 않는다.
        return true;
      }
    }
    return false;
  }
}

```

- `한 가지`만 한다는 규칙을 위반하지만, 차라리 checkPasswordAndInitializeSession이라는 이름이 더 낫다.

### 출력 인수

- 일반적으로 우리는 인수를 함수 **입력**으로 해석한다.
- 일반적으로 출력 인수는 피해야 한다.

```java
  // s는 입력인가? 출력인가?
  // 무언가에 s를 footer로 첨부할까? s에 footer를 첨부할까?
  appendFooter(s)

  // s가 출력이라는 사실은 분명하지만, 선언부를 보기 전까지 알 수 없다. 
  public void appendFooter(StringBuffer report)

  // good
  report.appendFooter()
```

## 명령과 조회를 분리하라

- 함수는 하나만 해야 한다.

```java
// bad
public boolean set(String attribute, String value)

if (set("username", "unclebob")) {
  ...
}
```

- 개발자는 "set"을 동사로 의도했지만, if 문에 넣고 보면 형용사로 느껴진다.
  - "username 속성이 unclebob으로 설정되어 있다면 ..."으로 읽힌다.
  - "username 속성을 unclebob으로 설정하는데 성공하면 ..."으로 읽히지 않는다.
  - 'setAndCheckIfExists'로 바꾸는 방법드 있지만, 여전히 어색하다.

- 명령과 조회를 분리해 혼란을 없앤다.

```java

// good
if (attributeExists("username")) {
  setAttribute("username", "unclebob");
  ...
}
```

## 오류 코드보다 예외를 사용하라

- 명령 함수에서 오류 코드를 반환하는 방식은 명령/조회 분리 규칙을 미묘하게 위반한다.
  - 자칫하면 if문에서 명령을 표현식으로 사용하기 쉬운 탓이다.

```java
if (deletePage(page) == E_OK) 
```

- 동사/형용사 혼란을 일으키지 않지만, 여러 단계로 중첩되는 코드를 야기한다.
- 오류 코드를 반환하면 호출자는 오류 코드를 곧바로 처리해야 한다는 문제에 부딪힌다.

```java
// bad
if (deletePage(page) == E_OK) {
  if (registry.deleteReference(page.name) == E_OK) {
    if (configKeys.deleteKey(page.name.makeKey()) == E_OK) {
      logger.log("page deleted");
    } else {
      logger.log("configKey not deleted");
    } 
  } else {
    logger.log("deleteReference from registry failed");
  } 
} else {
  logger.log("delete failed");
  return E_ERROR;
}
```

- 오류 코드 대신 예외를 사용하면 오류 처리 코드가 원래 코드에서 분리되므로 깔끔해진다.

```java
// good
try {
  deletePage(page);
  registry.deleteReference(page.name);
  configKeys.deleteKey(page.name.makeKey());
}
catch (Exception e) {
  logger.log(e.getMessage());
}
```

### Try/Catch 블록 뽑아내기

- try/catch 블록은 원래 추하다.
  - 코드 구조에 혼란을 일으키며, 정상 동작과 오류 처리 동작을 뒤섞는다.
  - try/catch 블록을 별도 함수로 뽑아내는 편이 좋다.

```java
public void delete(Page page) {
  try {
    deletePageAndAllReferences(page);
  }
  catch (Exception e) {
    logError(e);
  }
}

private void deletePageAndAllReferences(Page page) throws Exception {
  deletePage(page);
  registry.deleteReference(page.name);
  configKeys.deleteKey(page.name.makeKey());
}

private void logError(Exception e) {
  logger.log(e.getMessage());
}
```

- delete 함수가 모든 오류를 처리한다. 그래서 코드를 이핵하기 쉽다.
- 정상 동작과 오류 처리 동작을 분리하면 코드를 이해하고 수정하기 쉬워진다.

### 오류 처리도 한 가지 작업이다

- 함수는 '한 가지' 작업만 해야 한다.
- 오류 처리도 '한 가지' 작업에 속한다.
- 오류를 처리하는 함수는 오류만 처리해야 한다.

### Error.java 의존성 자석(Dependency Magnet)

- 오류 코드를 반환한다는 것은, class든 enum이든 어디선가 오류 코드를 정의한다는 뜻이다.

```java
public enum Error {
  OK,
  INVALID,
  NO_SUCH,
  LOCKED,
  OUT_OF_RESOURCES,
  WAITING_FOR_EVENT;
}
```

- 위와 같은 클래스는 의존석 자석이다.
  - 다른 클래스에서 Error enum을 import해야 하므로,
  - enum이 변한다면 Error enum을 사용하는 클래스 전부를 다시 컴파일하고 다시 배치해야 한다.
  - Error 클래스를 변경하기 힘들다.
- 오류 코드 대신 예외를 사용하면 새 예외는 Exception에서 파생하므로, 재컴파일/재배치 없이도 새 예외 클래스를 추가할 수 있다. (OCP)

## 반복하지 마라(Don't Repeat Yourself!)

- 중복을 없애면 가독성이 높아진다.
- 중복은 소프트웨어에서 모든 악의 근원이다.
- 많은 원칙과 기법이 중복을 없애거나 제어할 목적으로 나왔다.
  - 자료에서 중복을 제어할 목적으로 관계형 데이터베이스에 **정규 형식**을 만들었다. (by 커드(E.F.Codd))
  - 객체지향 프로그래밍: 코드를 부모 클래스로 몰아 중복을 없앤다.
  - 구조적 프로그래밍(Structured Programming), AOP(Aspect Oriented Programming), COP(Component Oriented Programming) -> 중복 제거 전략

## 구조적 프로그래밍(Structured Programming)

```text
* 데이크스트라(Edsger Dijkstra)의 구조적 프로그래밍 원칙
- 모든 함수와 함수 내 모든 블록에 entry와 exit가 하나만 존재햐야 한다. (Dijkstra)
- 즉, 함수는 return문이 하나여야 한다.
- 루프 안에서 break나 continue를 사용해선 안된다.
- goto는 절대로 사용해선 안 된다.
```

- 위 규칙은 함수가 클 때만 유용하다. (작을 때는 별이익이 없다.)
- 함수가 작을 때는 return, break, continue를 여러 차례 사용해도 괜찮다.
  - 오히려 단일 입출구 규칙(single-entry, single-exit rule)보다 의도를 표현하기 쉬워진다.
- goto는 작은 함수에서 피해야 한다.

## 함수를 어떻게 짜죠?

- 소프트웨더를 짜는 행위는 여느 글짓기와 비슷하다.
- 함수를 짤 때도 마찬가지다. 처음에는 길고 복잡하다. 들여쓰기 단계도 많고 중복된 루프도 많다. 인수 목록도 아주 길다. 이름은 즉흥적이고 코드는 중복된다. 하지만, 단위 테스트 케이스도 만든다.
- 그런 다음 코드를 다듬고, 함수를 만들고, 이름을 바꾸고, 중복을 제거한다. 메서드를 줄이고 순서를 바꾼다. 클래스를 쪼개기도 한다. 이 와중에 단위 테스트는 항상 통과한다.
- 최종적으로는 이 장에서 설명한 규칙을 따르는 함수가 얻어진다.
- 처음부터 탁 짜내지 않는다. 그게 가능한 사람은 없다.

## 결론

- 모든 시스템은 특정 응용 분야 시스템을 기술할 목적으로 프로그래머가 설계한 도메인 특화 언어(DSL, Domain Specific Language)로 만들어 진다.
- 함수는 그 언어에서 동사며, 클래스는 명사다.
- Master 프로그래머는 시스템을 (구현할) 프로그램이 아니라 (풀어갈) 이야기로 여긴다. 프로그래밍 언어라는 수단을 사용해 좀 더 풍부하고 좀 더 표현력이 강한 언어를 만들어 이야기를 풀어간다.
