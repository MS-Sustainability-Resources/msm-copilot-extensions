# Setup custom emission factor library with Copilot Actions and AI prompts 
## Use Case
Customers often need to configure custom emission factor libraries from PDF files and Excel. This use case demonstrates how Power Automate Action and AI prompts can be utilized to read PDF tables and configure a custom factor library in Microsoft Sustainability Manager (MSM). By creating a new flow in Power Automate that connects to the desired PDF or Excel file and using the AI Builder action to extract data from the tables, users can map the extracted data to MSM emission factor libraries. 
- Sample Query: I want to import custom emission factor library into Microsoft Sustainability Manager?

## Pre-Requisites
1.	Please ensure you have followed the general Pre-requisites [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/blob/main/README.md#pre-requisite).
2.	Turn on Generative Orchestration for an agent.Ref [here](https://learn.microsoft.com/en-us/microsoft-copilot-studio/advanced-generative-actions#turn-on-generative-orchestration-for-an-agent)

## High Level Flow

![image](https://github.com/user-attachments/assets/4131271a-1c28-4002-92be-ed03cec8e633)

## Implementation guidance
### I.	Import Power Automate flow:
  - Import ‘Custom Emission Factor Library’ Solution file from the source folder(setup_custom_factor_library\src\solution\CustomEmissionFactorLibrary.zip). Ref [here](https://learn.microsoft.com/en-us/power-apps/maker/data-platform/import-update-export-solutions)
  - In Powerautomate connections, [fix Dataverse connections used for the flow](https://learn.microsoft.com/en-us/power-automate/add-manage-connections#update-a-connection)
  - Open https://make.powerautomate.com/ and check the imported flow, if the flow is disabled, edit the flow. Fix connection reference errors.Save and publish the flow again.
  - This flow reads the PDF file from sharepoint. Configure the sharepoint and upload the sample PDF file in sharepoint. Please upload one- or two-pages PDF which has the emission factor library details. The sample NGER PDFs used can be found [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/tree/main/setup_custom_factor_library). Please ensure the flow points to this sharepoint.
### II.	Use custom connector actions with agents (preview)
Here we are using the [actions](https://learn.microsoft.com/en-us/microsoft-copilot-studio/advanced-plugin-actions#add-an-action) to respond to users automatically, using generative orchestration ( a preview feature announced in Nov24 ignite). Please ensure generative orchestration setting is switched on as given in pre-requisite.
Here we are configuring a Power automate flow as an action and describing the flow and  parameters it expects so the agent will infer when to call it.
  -	In copilot studio, Open Sustainability Copilot Bot, Goto Actions. Click on ‘Add an Action’ button. In the dialog box, Choose ‘Flow’ ,Choose the flow you wish to add as an action.
  -	Provide appropriate description for the action and parameters to enable the agent to use it.  The descriptions given for the sample custom factor library flows are given below.
    1.	Description for the Copilot: Use this action when user wants to import a custom emission factor library into Microsoft sustainability manager (msm).
   	2.	Factor Library Name: Please give the custom factory library name prefix
   	3.	Reference Data Type: Please give the plural name of the table of the reference data type that you would like to use for factor mappings
   	4.	PDF File Name: Please give the NGER PDF file name uploaded in your sharepoint. Please ensure that the PDF file has one page of emission factor that you wish to upload to an emission factor library
  - Save and Publish, to see your changes in MSM copilot.
### III.	Test:
-	Test with below sample query and give the required inputs asked.
    - a.I want to import custom emission factor library into Microsoft Sustainability Manager?
- You may need to connect to authorise during a session

## Alternate ways to implement
1.	Instead of using generative orchestration, we can configure the API Action to be called via a topic. With classic orchestration, an agent responds to users by triggering the topic whose trigger phrases match most closely the user's query

## Limitations
1.	The flow responds best when used with 1- or 2-page PDF as of now. It doesnt currently work with a huge pdf

