# Extend MSM Copilot to access the latest in public docs using custom topic and Bing Custom search
## UseCase
Use Case: Bringing latest Public Documentation into Microsoft Sustainability Manager Copilot
In this use case, we explore how the MSM Copilot can be extended to seamlessly integrate and search the latest public documentation, providing users with comprehensive and up-to-date information directly within the Microsoft Sustainability Manager. This ensures that users have access to critical knowledge without leaving the application, enhancing their productivity and decision-making capabilities.
- Query 1: What is the architecture of Microsoft sustainability manager?
- Query 2: What are the latest features released in Nov2024 for Microsoft sustainability manager?

## Pre-Requisite
Please ensure you have followed the general Pre-requisites [here](https://github.com/MS-Sustainability-Resources/msm-copilot-extensions/blob/main/README.md#pre-requisite).

## High-level flow
<img width="578" alt="image" src="https://github.com/user-attachments/assets/9c5ea545-1112-4dd8-8040-6b79a5f2fa1f">


## Implementation guidance
1.	In ‘Sustainability Copilot Bot’, Add a new topic.
2.	Open the ‘code editor’ in the topic 
 ![image](https://github.com/user-attachments/assets/12617c20-2338-4aae-af19-a04cd8ad333d)

3.	Copy paste the code from yaml file given in this source code here to your topic.
4.	Create a custom bing search and add the urls that you would like MSM copilot to access. Copy the custom configuration id. Instructions [here](https://learn.microsoft.com/en-us/bing/search-apis/bing-custom-search/how-to/quick-start#create-a-custom-search-instance)
5.	Copy the custom configuration id from the previous step and it to generative answers step. Instructions [here](https://learn.microsoft.com/en-us/microsoft-copilot-studio/nlu-generative-answers-bing)
6.	Publish after all your changes are done, to see your changes in MSM copilot.
7.	Test: Test with similar sample queries.
- 	Query 1: What is the architecture of Microsoft sustainability manager?
 - Query 2: What are the latest features released in Nov2024 for Microsoft sustainability manager?

## Alternate ways to implement
1.	You can try adding the url of the public website directly to the Knowledge Sources of the Copilot. But as of when this was written the website can only be specified to up to 2 levels of depth. 
2.	You can try adding the url of the public website directly to the Knowledge Sources of the Generative answers tab of the Copilot. But as of when this was written the website can only be specified to up to 2 levels of depth. 
3.	For now, in Dec-24 working with custom bing search yields better results

## Enhancements & Finetuning 
1. Customize Bing search for your needs
2.  For letting copilot know when to pick this Topic:  You can go to the topic ‘Sustainability Guide’ and modify the description/trigger phrases in the Trigger of the topic.
   

