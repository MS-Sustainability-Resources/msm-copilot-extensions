# Surface enterprise knowledge within MSM copilot with API actions

## Use Case
Use Case: Tapping Enterprise Knowledge via APIs
This use case demonstrates how MSM Copilot can harness the power of enterprise APIs to access vital organizational data. For example, when checking product services, a user may want to know the sales revenue of a specific product, such as a laptop, and identify the team responsible for it. By integrating with internal APIs that expose this information, MSM Copilot can provide instant, accurate insights, streamlining data retrieval processes and enhancing decision-making efficiency.
Sample Query: what is sales revenue for laptops in Contoso?

## Pre-Requisite
1.	Please ensure you have followed the general Pre-requisites here.
2.	Turn on Generative Orchestration for an agent- Orchestrate agent behavior with generative AI (preview) - Microsoft Copilot Studio | Microsoft Learn

## Implementation guidance
I.	Deploy Sample API:
There is a sample API code present in this repo under /invoke_enterprise_api/src/sample_api/product-sales-api. You can use this for demo.
Deploy the API in  App service, you can also use the free sku of app service also. You can follow any of the methods to deploy.
Az CLI, Azure portal, Visual Studio, Visual Studio code
II.	Use custom connector actions with agents (preview)
Here we are using the actions to respond to users automatically, using generative orchestration ( a preview feature announced in Nov24 ignite). Please ensure generative orchestration setting is switched on as given in pre-requisite.
Here we are configuring the API as an action and describing the API and parameters it expects so the agent will infer when to call it.
1.	In copilot studio, Open Sustainability Copilot Bot, Goto Actions. Click on ‘Add an Action’ button. In the dialog box, Choose ‘Custom Connector’  Add an API for custom connector’.
 
2.	You need to upload the your api’s Open API specification. The specification file for sample API code is given here-invoke_enterprise_api\src\sample_api\api_specification\products-api-specification.json
3.	Fill out the authentication if needed from API.
4.	For the sample api given in this repo, these were the outputs filled.
Description for the Copilot: “This action provides sales data for products in Contoso Organisation. Invoke this action to get information on revenue a product has and internal team owning the product”
III.	Test:
1.	Test with below sample query: 
what is sales revenue for laptops in Contoso?
2.	You may need to connect to authorise during a session
Alternate ways to implement
1.	Instead of using generative orchestration, we can configure the API Action to be called via a topic. With classic orchestration, an agent responds to users by triggering the topic whose trigger phrases match most closely the user's query

## Enhancements & Finetuning 
1.	You can also configure additional knowledge sources to provide enterprise knowledge. This can be via Documents, SharePoint or Dataverse. More details here.
