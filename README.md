# Hein.RulesEngine
Version 1 of Hein.RulesEngine

#### How does it work?
This Rules Engine leverages DynamoDB to save rule definitions and rules assoicated to those definitions.  An application that wants to apply rules to an object will create a `RuleRequest` json payload... do an HTTP POST to the Execute endpoint... the rules engine will take the Ruleset name (it's part of the payload)... grab all enabled rules from the definition/set... run thru them all... and respond back with the highest passing rule.

There's an admin site/screen that allows adminstrators to update rules by doing the following:  
1. Define entity/payload parameters
2. Create conditional statements from payload parameters  
3. And add resulting properties when all condition statements at met.

Hierarchy
1. Rule Definition  
a. Properties (Entity Definition)  
b. Rules [see Rule]  
2. Rule  
a. Enablement Flag  
b. Proiority Order  
c. Conditions
d. Property Results  


### Pros/Cons
| Pros | Cons |
| --- | --- |
| Easy to follow design for admins | Clunky, lots of point and click for configuration |
| Internal Functions help rule code gen | If you have really complex rules, yikes, nightmare to find the multiple you need to update |
| Rules are grouped together in one definiton |  |
