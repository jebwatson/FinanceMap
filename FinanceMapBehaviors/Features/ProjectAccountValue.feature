Feature: Project account value
    Scenario Outline: User requests account value on a future date given current income
        Given an account <Account>
        And a starting date <StartingDate>
        And a projection date <ProjectionDate>
        And an income <Income>
        When the user requests the account value
        Then the future account value is <FutureValue>
        
    Examples:
    | CurrentAccountValue | StartingDate | ProjectionDate | IncomeValue | IncomeFrequency | FutureAccountValue |
    | 500                 | 05/01/2021   | 06/01/2021     | 500         | 2               | 1500               |
    | 500                 | 05/01/2021   | 04/01/2021     | 500         | 2               | -500               |
    | 500                 | 05/01/2021   | 05/22/2021     | 500         | 2               | 1000               |
    | 500                 | 05/01/2021   | 05/19/2021     | 500         | 2               | 1000               |