Feature: ForecastApi

Background: 
	Given I know about the following forecast
	| ID | Date       | TemperatureC |
	| 1  | 2020-05-01 | 20           |
	| 2  | 2020-01-01 | 15           |
	| 3  | 2020-03-01 | 17           |
	| 4  | 2020-02-01 | 16           |
	| 5  | 2020-06-01 | 21           | 
	And And my joke provider will give me these jokes in order
	| joke |
	| joke1     |
	| joke2     |
	| joke3     |
	| joke4     |
	| joke5     |
	| joke6     |

Scenario: Authenticated user
	Given I am authenticated
	When I ask Brenda for a forecast
	Then My api returns a 200 status code
	And the results are
	| Date       | TemperatureC | TemperatureF | Summary |
	| 2020-01-01 | 15           | 58            | joke1   |
	| 2020-02-01 | 16           | 60            | joke2   |
	| 2020-03-01 | 17           | 62            | joke3   |
	| 2020-05-01 | 20           | 67            | joke4   |
	| 2020-06-01 | 21           | 69            | joke5   |

Scenario: Anonymous call
	Given I am not authenticated
	When I ask Brenda for a forecast
	Then My api returns a 302 status code