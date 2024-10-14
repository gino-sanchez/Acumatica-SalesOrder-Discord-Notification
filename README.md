# **Acumatica-Discord-Integration**
### Overview
**Acumatica-Discord-Integration** is a customization designed to send real-time notifications about Sales Orders from Acumatica ERP to a specified Discord channel using webhooks. This integration helps teams stay updated on new sales orders by posting detailed messages directly in Discord.

### Features
* Automatically sends a Discord notification when a new Sales Order is created in Acumatica.
* Includes order details such as Customer, Order Total, Order Date, and Line Items in the message.
* Supports pagination for large orders by sending multiple embeds if more than 10 items are present.
* Random colors are generated for each line item in the Discord embeds.

### Testing Notifications
* A Test Notification Button is available in the Discord Webhook Setup. This button automates the creation of 5 Sales Order documents and triggers a test notification to the Discord webhook, allowing you to verify that the integration is working as expected.
  
### Prerequisites
To use this customization, you will need:

* Access to an Acumatica ERP instance with customization capabilities.
* A Discord webhook URL (You can create one by following Discord's guide on creating webhooks).
### Installation
1. Clone or download this repository.
2. In Acumatica, navigate to **System** > **Customization Projects** > **Source Control** > **Open Project from Folder** > Locate to **_CustomizationProjects** > Double Click **DiscordIntegration** > Hit **OK** from the Dialog box.
3. Publish the customization project in Acumatica.
### Configuration
1. After installing the customization, navigate to the **Discord** Workspace > **Webhook Setup**
2. Enable the notifications feature.
3. Input the webhook URL provided by Discord.
4. Customize which sales orders trigger notifications by modifying the conditions in the code if necessary (Optional). By default, the trigger is upon save of the Sales Order document. 
### Usage 
Once set up, the customization works automatically when a Sales Order is created and saved. A webhook message is sent to the specified Discord channel, containing the following details:

* **Customer**: The name of the customer.
* **Order Total**: The total value of the sales order.
* **Order Date**: The date the order was created.
* **Order Description**: Any description added to the order.
* **Line Items**: Details of each item, including quantity, unit price, and extended price, sent in paginated embeds (if more than 10 lines).

### Troubleshooting
* If notifications aren't being sent, ensure that:
  * The Discord Webhook URL is correctly configured.
  * Notifications are enabled in the Discord Setup screen.
  * The Sales Order meets the trigger conditions in the overridden Persist method.
 
### License
This project is licensed under the MIT License.


###### © 2024 Gino Sanchez. All rights reserved.

