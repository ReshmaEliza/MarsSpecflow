Feature: This test suite contains test scenarios for Skill tab.
Background: 
Given I log into the portal with UserName '' and Password '' and navigate to 'Skills' Tab
And User has no skill in their profile
 


@Skill
Scenario: TC_001. Create a new Skill record
	When I create a new skill record 'Test' 'Intermediate'
	Then the skill should be saved as 'Test'

	Scenario:TC_002,Tc_003,TC_004,TC_005,TC_006. Create a new Skill record with invalid characters 
	When I create a new skill record '#' 'Beginner'
	Then the skill should not be saved '#'
	
	

	Scenario Outline: TC_007. Update a Skill 
	   Given the user profile is set up with the Skills:
    | Skillvalue | Level  |
    | Test Engineer | Intermediate  |
    | Develper  | Intermediate  |

	When the user wants to update the Skill or level from "Test Engineer","Intermediate" to "AA","Intermediate"
  Then the update from skill  "Test Engineer","Intermediate" to skill "AA","Intermediate" is possible
	



	Scenario Outline: TC_008. Delete a Skill 
	   Given the user profile is set up with the Skills:
    | Skillvalue | Level  |
    | French   | Expert  |
    | test Analyst  | Beginner |
 | c#    | Intermediate  |
 | Test    | Intermediate |


	When the user wants to delete the Skill  "French"
  Then the Skill "French" should be deleted.
	

	Scenario:  TC_009,TC_010 Duplicate Entry Check for Addition of Skill
Given the user profile is set up with the Skills:
    | Skillvalue | Level  |
    | Test Engineer  | Beginner  |
 | Graphic Designer | Beginner  |

	When I try to create another record with same skills 'test Engineer' 'Expert'
	Then Adding of second record for skill 'test Engineer' 'Expert' fails 
	


Scenario:TC_011, Duplicate Entry Check while updating a Skill
Given the user profile is set up with the Skills:
    | Skillvalue | Level  |
    | Graphic Designer  | Beginner  |
    | Java developer  | Expert |
 | Test Engineer  | Beginner |
 When the user wants to update the Skill or level from "Graphic Designer","Beginner" to "Test Engineer","Beginner"
 Then the system should block the skill updation from 'Graphic Designer' to 'Test Engineer'.

	Scenario: TC_012 Validate the addition of Skill feature with 1000 characters
	Given User has no skill in their profile
	When I create a new Skill with 1000 random characters and level 'Expert'
	Then the addition of Skill with more than 50 characters should fail
	

	Scenario: TC_013  Verify the stability of system under high load
	Given User has no skill in their profile
	When I create a  10 new random skill with level 'Expert' set for the user.
	Then verify if all the 10 elements is added to the system
	

