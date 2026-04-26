# Blackboard projects in Azure Integration



## Project 1: Basic SIS integration 





### Technology: Logic App, Azure API management, Service bus, Application Insights, Log Analytics Workspace, SQL server 



### Work flow: Different files is sent to different endpoint in Azure API management. Requests will be sent to Service bus and Logic app will listen for new messages at certain time of a day. Mock data is saved in SQL server. Application Insights and Log Analytics Workspace keep logs of all component. 



### Problem description: Sending feed files to Blackboard SIS (Student Integration System) endpoints. Use a list of data validation to make sure the feed files are having correct format. For files less than 5 rows, check if the courses area available in Blackboard and then send as soon as the file is received. For files more than 5 rows, save the as a blob and send it by batch in the end of the day. 



### Improvement: User C# script to replace the list of data validation instead of blocks in 



## Project 2: Reimplementation of LTI (Learning Tools Interoperability) in Blackboard



### Azure API management, Azure function



### Work flow: A request for a new course is initiated from Blackboard. API management receives a request and forward it to Azure function. This function receives the request does the authentication and data transformation/extraction. A response is sent back to another Azure API management endpoint to create a course (Project 1). 



### Problem description: the current LTI tool is a .NET C# web app that runs all days but is only used several times per week. Azure function is a perfect use case for this on-demand app. 



### Improvement: Log the list of courses that have been created. 



## Project 3: Prevent duplicate account in various system



### Technology: Event grid, service bus, azure function  



### Work flow: whenever a duplicate email is created, a new message is created to Event Grid topic. This is pushed to various subscriptions of different app. If duplication can be resolved immediately, the message is pushed to a Logic App or Azure Function. If it can't be resolved immediately, a message is queued to a service bus topic. When a number of messages is reached, it will send an email to a supplier asking for resolving this duplication. 



### Problem description: Many system depend on the record on Blackboard to create users. However, user mail is not unique, which can lead to data breach. Not all systems can resolve this duplication automatically. So, each of them only need to subscribe to a topic to take appropriate action when required. 



### Improvement: Create a column for each subscription to report on the status of each duplication record. 



## Project 4: Provision users from Blackboard to YuJa  



### Technology: Powershell, API Management, Azure function



### Work flow: Powershell script runs a query daily to check for changes in the status of users in Blackboard. It create a file and submit to an endpoint in API management. This request is then forwarded to Azure function run API requests to YuJa to change students' status. 



### Problem description: To prevent disabled student in Blackboard to access YuJa and to allow returning students to access their old account. 



### Improvement: Can make use of Event Grid to provide pushed messages to different systems subscribing this topic. 



## Project 5: Blackboard API batch API process with version control



### Technology: Azure function, API management, SQL server



### Work flow: A file is sent to an API management endpoint. It is forwarded to an Azure function which then save data into a table including what action is required. Another function is then called to pick up these records and execute the action on Blackboard. It will write a response ID and response status to each of the row. Users can then use these IDs to reverse the action if required.  



### Problem description: Instead of using Postman cron job to execute Blackboard API calls, this project record logs and response ID of all API request for troubleshooting and version control. 



### Improvement: Currently only Insert action is implemented. Also consider Edit action as well









