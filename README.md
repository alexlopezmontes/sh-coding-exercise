## General instructions

This project was a technical evaluation for a Sr Software Engineering position.  The following scenario was presented:

Scenario:
We have implemented a background process responsible for monitoring order status with the objective of identifying orders that have reached a 'delivered' state. Upon detection of such orders, a delivery notification is dispatched through a secondary API. After the notification is sent, we then update the order record via API.

Regrettably, the developer responsible for delivering this essential functionality won the lottery and quit the next day. On their departure, they forwarded us a segment of code pertaining to this task. While we have reasonable confidence in its functionality, a preliminary review indicates deviations from established best practices. Adding to the complexity, the absence of the Product Owner (PO) due to vacation without access to cellular communication means that clarifications on the code or its requirements cannot be promptly obtained.

This situation places us at a crossroads, as the business is eager to expedite the integration of this feature into the production environment. Thus, we're venturing forth to revisit and refine the provided code. Your job is to fix up the code, make sure it fits current coding best practices and make the code more supportable



- Set the Worker project as the Startup project.
- Ensure you have obtained the necessary NuGet packages.
- Note that I have simulated responses from the APIs for unit tests but haven't done so for when the worker runs.

### Comments for OrderApiGateway
- I wasn't sure if we'd hipotetically have control over the API we consume. If so, I'd recommend adding an enpoint that allows us filter by status, such as GET - api/orders?status=Delivered.

### Comments for Orders Worker
- The idea is to add a Background job processing framework such as hangfire.  I felt that adding too much robustness might delay the application submission, so I've kept it relatively simple for now.
 
### Comments For Utility
- I'm naming the folder as Constants for readability although I'm using static readonly strings to not having the compiler create a copy of the constant value on each assembly.

### For Settings
- My preference is to define the environment-specific setting to be copied by CI/CD pipelines, instead of creating  appsettings.{environment}.json files.
