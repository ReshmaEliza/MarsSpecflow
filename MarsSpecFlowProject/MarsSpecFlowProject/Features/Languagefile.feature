
Feature: This test suite contains test scenarios for language feature.
Background: 
Given I log into the portal with UserName '' and Password '' and  navigate to 'Languages' Tab
And User has no language in their profile

@language
Scenario: TC_001. Create a new language record with valid characters
	When I create a new language record 'French' 'Basic'
	Then the record should be saved 'French'

Scenario: TC_002,TC_003,TC_004,TC_005,TC_006. Create a new language record with invalid characters 
When I create a new language record '<language>' '<Level>'
Then the record should not be saved '<language>'
Examples: 
| language | Level  |
| @@@      | Basic  |
|""| Fluent |
|       | Fluent |



Scenario Outline: TC_007. Update a language 
 	   Given the user profile is set up with the languages:
  | Languages | Level  |
  | French   | Basic  |
  | English  | Basic  |

When the user wants to update the language or level from "French","Basic" to "Mal","Basic"
Then the update from  "French","Basic" to "Mal","Basic" is possible

  
Scenario Outline: TC_008. Delete a language 
 	   Given the user profile is set up with the languages:
  | Languages | Level  |
  | French   | Basic  |
  | English  | Fluent |
  | dutch    | Basic  |
  | Hindi    | Fluent |


When the user wants to delete the language  "French"
Then the language "French" should be deleted.


Scenario:  TC_009,TC_010 Duplicate Entry Check for Addition of language
 Given the user profile is set up with the languages:
  | Languages | Level  |
  | French   | Basic  |
  | English  | Fluent |
When I try to create another record with same value 'French' 'Fluent'
Then Adding of second record 'French' 'Fluent' fails 


Scenario: TC_011, Duplicate Entry Check while updating a language
 Given the user profile is set up with the languages:
  | Languages | Level  |
  | English  | Basic  |
  | French   | Fluent |
  | Hindi    | Basic  |
  When the user wants to update the language or level from "French","Fluent" to "English","Fluent"
  Then the system should block the updation from 'French' to 'English'.


 Scenario: TC_012. Verify the Addition of Language in Two Sessions
And I open a second session in tab 2.
And the user profile is set up with the languages in Session 1:
  | Languages | Level  |
  | French   | Basic  |
  | English  | Fluent |
  | Dutch    | Basic  |
  | Hindi    | Fluent |
When I create a new language record 'German' 'Basic' in Session 2
Then the entry of 'German','Basic' should be blocked.


Scenario: TC_013 Validate the addition of language feature with 1000 characters
When I create a new language with 1000 random charcaters and level 'Basic'
Then the addition of language with more than 50 characters should fail



 
  


