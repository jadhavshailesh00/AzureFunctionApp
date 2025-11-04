# BankFunctionsApp - demo Azure Functions for banking domain

## Prerequisites
- Visual Studio 2022 with Azure Development workload
- Azure Functions Core Tools (installed by VS)
- Azure subscription (for deployment)
- Storage emulator or real storage account

## Run locally
1. Open the solution in Visual Studio.
2. Set `local.settings.json` with connections.
3. Press F5 to start the Functions host.
4. Test HTTP endpoints:
   - POST http://localhost:7071/api/customers/register
   - POST http://localhost:7071/api/transfers/initiate

## Deploy
- Right-click project > Publish > Azure > Azure Function App
- Fill subscription/resource group/app name details and deploy.

## Notes
- Replace all stubbed services with production-grade implementations:
  - AccountService → transactional ledger / DB with concurrency control
  - CustomerService → CosmosDB/SQL
  - NotificationService → Twilio, SendGrid, etc.
  - Add authentication/authorization (Azure AD, managed identities)
  - Add monitoring, logging, tracing (App Insights)
  - Implement idempotency and dead-letter handling for queues
