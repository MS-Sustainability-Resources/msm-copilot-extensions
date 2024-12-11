# Surface enterprise knowledge within MSM copilot with API actions

## Use Case
Use Case: Tapping Enterprise Knowledge via APIs
This use case demonstrates how MSM Copilot can harness the power of enterprise APIs to access vital organizational data. For example, when checking product services, a user may want to know the sales revenue of a specific product, such as a laptop, and identify the team responsible for it. By integrating with internal APIs that expose this information, MSM Copilot can provide instant, accurate insights, streamlining data retrieval processes and enhancing decision-making efficiency.
 - Sample Query: what is sales revenue for laptops in Contoso?

## Pre-Requisite
1.	Please ensure you have followed the general Pre-requisites [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/blob/main/README.md#pre-requisite).
2.	[Turn on Generative Orchestration for an agent](https://learn.microsoft.com/en-us/microsoft-copilot-studio/advanced-generative-actions#turn-on-generative-orchestration-for-an-agent)

## High Level flow
<img width="703" alt="image" src="https://github.com/user-attachments/assets/e6622a64-e96d-4044-b4d1-1e00c45bf444">


## Implementation guidance
### I.	Deploy Sample API:
There is a sample API code present in this repo under /invoke_enterprise_api/src/sample_api/product-sales-api. You can use this for demo.
Deploy the API in  App service, you can also use the free sku of app service also. You can follow any of the methods to deploy.
- [Az CLI](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-cli#2-publish-your-web-app)
- [Azure portal](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-azure-portal#2-publish-your-web-app)
- [Visual Studio](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vs#2-publish-your-web-app)
- [Visual Studio code](https://learn.microsoft.com/en-us/azure/app-service/quickstart-dotnetcore?tabs=net80&pivots=development-environment-vscode#2-publish-your-web-app)
### II.	Use custom connector actions with agents (preview)
Here we are using the [actions](https://learn.microsoft.com/en-us/microsoft-copilot-studio/advanced-plugin-actions#add-an-action) to respond to users automatically, using generative orchestration ( a preview feature announced in Nov24 ignite). Please ensure generative orchestration setting is switched on as given in pre-requisite.
Here we are configuring the API as an action and describing the API and parameters it expects so the agent will infer when to call it.
1.	In copilot studio, Open Sustainability Copilot Bot, Goto Actions. Click on ‘Add an Action’ button. In the dialog box, Choose ‘Custom Connector’.
![image](https://github.com/user-attachments/assets/9289a468-d69c-47db-9b95-e78f7e177a64)
2.	You need to upload the your api’s Open API specification. The specification file for sample API code is given here	[invoke_enterprise_api\src\sample_api\api_specification\products-api-specification.json](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/blob/main/invoke_enterprise_api/src/sample_api/api_specification/products-api-specification.json)
3.	Fill out the authentication if needed from API.
4.	For the sample api given in this repo, this was the description used
  -  Description for the Copilot: “This action provides sales data for products in Contoso Organisation. Invoke this action to get information on revenue a product has and internal team owning the product”
5. Save and Publish, to see your changes in MSM copilot.
- ### III.	Test:
1.	Test with below sample query: 
what is sales revenue for laptops in Contoso?
2.	You may need to connect to authorise during a session

## Alternative Implementations
1.	Instead of using generative orchestration, we can configure the API Action to be called via a topic. With classic orchestration, an agent responds to users by triggering the topic whose trigger phrases match most closely the user's query

## Enhancements & Finetuning 
1.	You can also configure additional knowledge sources to provide enterprise knowledge. This can be via Documents, SharePoint or Dataverse. More details [here](https://learn.microsoft.com/en-us/microsoft-copilot-studio/knowledge-copilot-studio#supported-knowledge-sources).
2.	Configure multiple API actions 

