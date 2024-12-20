# Solve ingestion errors with MSM Copilot using custom topics and actions
## Use Case
When importing data into Microsoft Sustainability Manager, users face errors due to missing reference data. Traditionally, resolving these errors requires navigating through various screens to identify and rectify the missing or incorrect data. Now we can extend MSM copilot to solve such errors in their chat, with AI helping them with similar reference data found in system. This would help the users more easily identify whether they need to insert their data or change the incoming data.
### Benefits
  - Efficiency: Users can resolve data import errors more quickly by following the streamlined guidance provided by the MSM Copilot, reducing the time spent navigating multiple screens.
  - Accuracy: The chat-based guidance helps users follow the correct steps to resolve errors, minimizing the risk of additional mistakes.
- Sample Query: I want to resolve import errors in Microsoft sustainability manager.

## Pre-Requisite
Please ensure you have followed the general Pre-requisites [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/blob/main/README.md#pre-requisite).

## High Level flow
<img width="722" alt="image" src="https://github.com/user-attachments/assets/c04c2810-c966-4adc-a3a6-c7fb27777b4d">


## Implementation guidance
### I.	Add topics:
1.	In ‘Sustainability Copilot Bot’, Add a new topic.
2.	Open the ‘code editor’ in the topic 
 ![image](https://github.com/user-attachments/assets/67dbb116-265b-41b8-bf35-fb93fd44017c)

3.	Copy paste the code from MSM_error_guide.yaml file given in this source code [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/tree/main/solve_ingestion_error/src/topics) to your topic.
4.	Repeat the steps to add another topic - ingestion_error_resolver.yaml

### II.	Import flows & AI models
1.	Import ‘Sustainability Copilot Ingestion Error Resolver’ Solution file from the source folder (solve_ingestion_error\src\solution\SustainabilityCopilotIngestionErrorResolver.zip). Ref- [Import solutions](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/import-update-export-solutions)
2.	The solution has 4 flows and 3 AI prompts necessary for this use case.
3.	In Powerautomate connections, fix connection for the flows - [Manage connections](https://learn.microsoft.com/en-us/power-automate/add-manage-connections#update-a-connection)
4.	Open https://make.powerautomate.com/ and check the imported flows, if any flow is disabled, edit the flow. Fix connection reference errors.Save and publish the flow again.

### III.	Setup authentication
1.	We would need to setup authentication for making Dataverse API calls from our topic. For this setup an Azure App registration - [Tutorial: Register an app with Microsoft Entra ID  - Power Apps](https://learn.microsoft.com/en-us/power-apps/developer/data-platform/walkthrough-register-app-azure-active-directory).Make sure you have granted permission to Dataverse API as described.
2.	In the app registration -Add the redirect URI. ’https://token.botframework.com/.auth/web/redirect’.   Ref- [Quickstart: Register an app in the Microsoft identity platform](https://learn.microsoft.com/en-us/entra/identity-platform/quickstart-register-app?tabs=certificate#add-a-redirect-uri) 
3.	Go to Manage -> ’Certificates & Secrets’. Create a new client secret and copy and keep the value for use in the next step.

### IV.	 Modify Copilot’s authentication
1.	Copilot’s Authentication needs to be set up with the app registration created in previous step to enable copilot to call Dataverse API’s
2.	Go to the Copilot -> Settings  -> Security  -> Authentication -> Authenticate Manually.
a.	Service provider – Select  ‘Azure Active Directory v2’.
b.	Client Id -Copy the client id in the app registration created in previous step.
c.	Client Secret- Copy the Client Secret value copied in previous step.
d.	Scope- Give the value ‘profile openid https://org5231f668.crm.dynamics.com//.default’

### V.	 Deploy Azure functions
1.	There is custom Azure functions code present to format and filter the list of ingestion errors returned by the API call in table format. This is under [/solve_ingestion_error/src/azure_function/filter-ingestion-errors](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/tree/main/solve_ingestion_error/src/azure_function/filter-ingestion-errors) .
2.	Navigate to the code and deploy it to Azure Functions. You can choose the Azure Functions Consumption Sku. You can use any of the methods for deployment.
     - [Azure CLI](https://learn.microsoft.com/en-us/cli/azure/functionapp/deployment/source#az-functionapp-deployment-source-config-zip)
     - [Visual Studio Code publish](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs-code?tabs=node-v4%2Cpython-v2%2Cisolated-process%2Cquick-create&pivots=programming-language-csharp#republish-project-files)
     - [Visual Studio publish](https://learn.microsoft.com/en-us/azure/azure-functions/functions-develop-vs?pivots=isolated#publish-to-azure)
4.	Copy the deployed Azure function url
   
### VI.	Modify topic ‘Ingestion error resolver’
1.	In Copilot Studio, Go to the Topic – ‘Ingestion error resolver’.
2.	In the HTTP call ‘Fetch Ingestion Errors Service’. Replace your orgname in '[orgname].crm.dynamics.com' in the HTTP url.
3.	In the HTTP call ‘HTTP Request -Extract Errors’. Replace your Azure Function url from previous step in  place of 'FunctionUrl' in the HTTP url.
4.	Save and publish all your changes , to see your changes in MSM copilot.
   
### VII.	Test:
2.	Test with below sample query and choose options when provided with the same.
- I want to resolve import errors in Microsoft sustainability manager.

## Enhancements & Finetuning
1. Extend this to solve other error scenarios.
2. Modify the topic to change options & flow for the user.
3. Modify the description/trigger phrases in the Trigger of the topic

## Limitations
1. Responses & Reference matches are found by AI, some times there may be a delay in fetching the responses.
