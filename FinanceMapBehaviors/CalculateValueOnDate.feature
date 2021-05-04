Feature: Calculate account value on a given date
    
    @mytag
    Scenario: Calculate future account value
        Given the starting account value
        And the future date
        When the user requests the account value
        Then the account value is returned