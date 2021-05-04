Feature: Project account value
    Scenario: User requests account value on a future date given current income
        Given an account object
        And 500 dollars as the current value
        And 4 weeks from today as the future date
        And an income object
        And 2 weeks as the frequency
        And 500 dollars as the value
        When the user requests the account value
        Then 1500 dollars is the future account value
        
    Scenario: User requests account value on a past date given current income
        Given an account object
        And 500 dollars as the current value
        And -4 weeks from today as the future date
        And an income object
        And 2 weeks as the frequency
        And 500 dollars as the value
        When the user requests the account value
        Then -500 dollars is the future account value
        
    Scenario: User requests account value three weeks from now given current income
        Given an account object
        And 500 dollars as the current value
        And 3 weeks from today as the future date
        And an income object
        And 2 weeks as the frequency
        And 500 dollars as the value
        When the user requests the account value
        Then 1000 dollars is the future account value