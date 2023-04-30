# 10장. 클래스

## 클래스 체계

표준 자바 관례(convention)에 따르면,

- 1. 가장 먼저, 변수 목록이 나온다.
  - static public, static private 순으로 나오며
  - 그 다음 private instance variable (public instance variable이 필요한 경우는 거의 없다.)
- 2. 그 다음 public function이 나온다.
  - private function은 자기 자신을 호출한 public function 직후에 넣는다.
  - 즉, 추상화 단계까 순차적으로 내려간다.
  - 그래서 프로그램은 신문 기사처럼 읽힌다.

## 캡슐화(encapsulation)

- 변수나 유틸리티 함수는 가능한 private을 사용한다.
- 가끔은 protected로 선언해 테스트 코드에 접근을 허용하기도 한다.
- 하지만, private 상태를 유지할 온갖 방법을 강구한다. 캡슐화를 풀어주는 결정은 언제나 최후의 수단이다.

## 클래스는 작아야 한다

- 클래스를 만들 때 **첫 번째 규칙은 크기다**. 클래스는 작아야 한다.
- 두 번째 규칙도 크기다. 더 작아야 한다.
- 함수는 물리적인 행 수로 크기를 측정했지만, **클래스는 맡은 책임**을 센다.

```java
// 목록10-2 충분히 작을까?
public class SuperDashboard extends JFrame implements MetaDataUser {
    public Component getLastFocusedComponent()
    public void setLastFocused(Component lastFocused)
    public int getMajorVersionNumber()
    public int getMinorVersionNumber()
    public int getBuildNumber()
}
```

- 클래스 이름은 해당 클래스 책임을 기술해야 한다.
  - 실제로 작명은 클래스 크기를 줄이는 첫 번째 관문이다.
  - 클래스 이름이 모호하다면, 클래스 책임이 너무 많아서다. (클래스 이름에 Processor, Manager, Super 등과 같이 모호한 단어가 있다면 클래스에다 여러 책임을 떠안겼다는 증거다.)

- 클래스 설명은 만일("if"), 그리고("and"), -(하)며("or"), 하지만("but")을 사용하지 않고서 25단어 내외로 가능해야 한다.
- 목록10-2는 "SuperDashboard는 마지막으로 포커스를 얻었던 컴포넌트에 접근하는 방법을 제공하며, 버전과 빌드 번호를 추적하는 메커니즘을 제공한다."
  - 첫 번째 "~하며"는 SuperDashboard에 책임이 너무 많다는 증거다.

### 단일 책임 원칙(Single Responsibilty Principle)

- 단일 책임 원칙은 클래스나 모듈을 **변경할 이유**가 하나, 단 하나뿐이어야 한다는 원칙이다.
- SRP는 '책임'이라는 개념을 정의하며 적절한 클래스 크기를 제시한다.
- 클래스는 책임, 즉 변경할 이유가 하나여야 한다는 의미다.

- 겉보기에는 작지만 목록 10-2 SuperDashboard는 변경할 이유가 두 가지다.
  - 첫째, SuperDashboard는 소프트웨어 버전 정보를 추적한다. 그런데 버전 정보는 소프트웨어를 출시할 때마다 달라진다.
  - 둘째, SuperDashboard는자바 스윙 컴포넌트를 관리한다. (SuperDashboard는 최상위 GUI 윈도의 스윙 표현인 JFrame에서 파생한 클래스다.) 즉, 스윙 코드를 변경할 때마다 버전 번호가 달라진다.
- 책임, 즉 변경할 이유를 파악하려 애쓰다 보면 코드를 추상화하기도 쉬워진다. 더 좋은 추상화가 더 쉽게 떠오른다.
  - SuperDashboard에서 버전 정보를 다르는 메서드를 따로 빼내 Version이라는 독자적인 클래스를 만든다.
  - 이 클래스는 다른 애플리케이션에서 재사용하기 아주 쉬운 구조다!

```java
// 목록 10-3 단일 책임 클래스
public class Version {
    public int getMajorVersionNumber()
    public int getMinorVersionNumber()
    public int getBuildNumber()
}
```

- SRP는 객체 지향 설계에서 더욱 중요한 개념이다. 또한 이해하고 지키기 수월한 개념이기도 하다.
- 하지만, SRP는 클래스 설계자가 가장 무시하는 규칙 중 하나다.
  - 소프트웨어를 돌아가게 만드는 활동과 소프트웨어를 깨끗하게 만드는 활동은 완전히 별개다.
  - 우리들 대다수는 두뇌 용량에 한계까 있어 '깨끗하고 체계적인 소프투웨어'보다 '돌아가는 소프트웨어'에 초점을 맞춘다. 전적으로 올바른 태도다. 관심사를 분리하는 작업은 프로그램만이 아니라 프로그래밍 활동에서도 마찬가지로 중요하다.
- 문제는 우리들 대다수가 프로그램이 돌아가면 일이 끝났다고 여기는 데 있다.
  - '깨끗하고 체계적인 소프투웨어'라는 다음 관심사로 전환하지 않는다. 프로그램으로 되돌아가 만능 클래스를 단일 책임 클래스 여럿으로 분리하는 대신 다음 문제로 넘어가버린다.
- 많은 개발자는 자잘한 단일 책임 클래스가 많아지면 큰 그림을 이해하기 어려워진다고 우려한다.

- 큰 클래스가 몇 개가 아니라 작은 클래스 여럿으로 이뤄진 시스템이 더 바람직하다.
  - 작은 클래스는 각자 맡은 책임이 하나며, 변경할 이유가 하나며, 다른 작은 클래스와 협력해 시스템에 필요한 동작을 수행한다.

### 응집도(Cohesion)

- 클래스는 인스턴스 변수 수가 작아야 한다. 각 클래스 메서드는 클래스 인스턴스 변수를 하나 이상 사용해야 한다.
- 일반적으로 메서드가 변수를 더 많이 사용할수록 메서드와 클래스는 응집도가 더 높다. 모든 인스턴스 변수를 메서드마다 사용하는 클래스는 응집도가 가장 높다.
- 응집도가 가장 높은 클래스는 가능하지도 바람직하지도 않다. 그렇지만 우리는 응집도가 높은 클래스를 선호한다. 응집도가 높다는 말은 클래스에 속한 메서드와 변수가 서로 의존하며 논리적인 단위로 묶인다는 의미이기 때문이다.

```java
// 목록 10-4 Stack.java 응집도가 높은 클래스
public class Stack {
    private int topOfStack = 0;
    List<Integer> elements = new LinkedList<Integer>();
    
    public int size() {
        return topOfStack;
    }

public void push(int element) {
    topOfStack++;
    elements.add(element);
    }

    public int pop() throws PoppedWhenEmpty {
        if (topOfStack == 0)
            throw new PoppedWhenEmpty();
        int element = elements.get(--topOfStack);
        elements.remove(topOfStack);
        return element;
    }
}
```

- '함수를 작제, 매개변수 목록을 짧게'라는 전략을 따르다 보면 때때로 몇몇 메서드만이 사용하는 인스턴스 변수가 아주 많아진다.
  - 이는 새로운 클래스로 쪼개야 한다는 신호다.
- 응집도가 높아지도록 변수와 메서드를 적절히 분리해 새로운 클래스 두세 개로 쪼개준다.

### 응집도를 유지하면 작은 클래스 여럿이 나온다

- 큰 함수를 작은 함수 여럿으로 쪼개다 보면 종종 작은 클래스 여럿으로 쪼갤 기회가 생긴다.

- 큰 함수를 작은 함수/클래스로 쪼갤때, 프로그램 길이가 늘어난 이유
  - 첫째, 리팩터링한 프로그램은 좀 더 길고 서술적인 변수 이름을 사용한다.
  - 둘째, 리팩터링한 프로그램은 코드에 주석을 추가하는 수단으로 함수 선언과 클래스 선언을 활용한다.
  - 셋째, 가독성을 높이고자 공백을 추가하고 형식을 맞추었다.

## 변경하기 쉬운 클래스

```java
// 목록 10-9 변경이 필요해 '손대야' 하는 클래스
public class Sql {
  public Sql(String table, Column[] columns)
  public String create()
  public String insert(Object[] fields)
  public String selectAll()
  public String findByKey(String keyColumn, String keyValue)
  public String select(Column column, String pattern)
  public String select(Criteria criteria)
  public String preparedInsert()
  private String columnList(Column[] columns)
  private String valuesList(Object[] fields, final Column[] columns)
  private String selectWithCriteria(String criteria)
  private String placeholderList(Column[] columns)
}
```

- 새로운 SQL 문을 지원하려면 반드시 Sql 클래스에 손대야 한다.

```java
// 목록 10-10 닫힌 클래스 집합
abstract public class Sql {
  public Sql(String table, Column[] columns)
  abstract public String generate();
}

public class CreateSql extends Sql {
  public CreateSql(String table, Column[] columns)
  @Override public String generate()
}

public class SelectSql extends Sql {
  public SelectSql(String table, Column[] columns)
  @Override public String generate()
}

public class InsertSql extends Sql {
  public InsertSql(String table, Column[] columns, Object[] fields)
  @Override public String generate()
  private String valuesList(Object[] fields, final Column[] columns)
}

public class SelectWithCriteriaSql extends Sql {
  public SelectWithCriteriaSql(
  String table, Column[] columns, Criteria criteria)
  @Override public String generate()
}

public class SelectWithMatchSql extends Sql {
  public SelectWithMatchSql(
  String table, Column[] columns, Column column, String pattern)
  @Override public String generate()
}

public class FindByKeySql extends Sql
  public FindByKeySql(
  String table, Column[] columns, String keyColumn, String keyValue)
  @Override public String generate()
}

public class PreparedInsertSql extends Sql {
  public PreparedInsertSql(String table, Column[] columns)
  @Override public String generate() {
  private String placeholderList(Column[] columns)
}

public class Where {
  public Where(String criteria)
  public String generate()
}

public class ColumnList {
  public ColumnList(Column[] columns)
  public String generate()
}
```

- 목록 10-10은 SRP도 지원하지만, 또 다른 핵심 원칙인 OCP(Open-Closed Principle)도 지원한다.
  - OCP: 클래스는 확장에 개방적이고 수정에 폐쇄적이어야 한다는 원칙
- 새 기능을 수정하거나 기존 기능을 변경할 때 건드릴 코드가 최소인 스스템 구조가바람직하다.
- 이상적인 시스템이라면 새 기능을 추가할 때 시스템을 확장할 뿐 기존 코드를 변경하지 않는다.

## 변경으로부터 격리

- 요구사항은 변하기 마련이다.
- 상세한 구현에 의존하는 코드는 테스트가 어렵다.

```java
public interface StockExchange {
  Money currentPrice(String symbol);
}

public Portfolio {
  private StockExchange exchange;
  
  public Portfolio(StockExchange exchange) {
    this.exchange = exchange;
  }
  
  // ...
}
```

```java
public class PortfolioTest {
  private FixedStockExchangeStub exchange;
  private Portfolio portfolio;
  
  @Before
  protected void setUp() throws Exception {
    exchange = new FixedStockExchangeStub();
    exchange.fix("MSFT", 100);
    portfolio = new Portfolio(exchange);
  }
  
  @Test
  public void GivenFiveMSFTTotalShouldBe500() throws Exception {
    portfolio.add(5, "MSFT");
    Assert.assertEquals(500, portfolio.value());
  }
}
```

- 시스템의 결합도를 낮추면 유연성과 재사용성도 더욱 높아진다.
- 결합도가 낮다는 소리는 각 시스템 요소가 다른 요소로 부터 그리고 변경으로부터 잘 격리되어 있다는 의미다.
- 결합도를 최소로 줄이면 DIP(Dependecy Inversion Priciple)을 따라는 클래스가 나온다.
  - DIP: 클래스가 상세한 구현이 아니라 추상화에 의존해야 한다는 원칙
  - 위 예시ㅣ에서 Protfolio 클래스는 TokyoStockExchange라는 상세한 구현 클래스가 아니라 StockExchange 인터페이스에 의존한다.
- 추상화로 실제로 주가를 얻어오는 출처나 얻어오는 방식 등과 같은 구체적인 사실을 모두 숨긴다.
