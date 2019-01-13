# Test Plan

## Build and deploy
  [x] Plain function app
  [x] Plain function app with no webjobs dashboard
  [x] Plain function app with no webjobs dashboard, add app insights
  [x] Function app with EH output, no webjobs + appinsights

## Test
  [x] Plain
  [x] Plain no webjobs
  [x] Plain appinsights
  [x] EH

## Notes
- Azure Devops load test fails when trying to target default deployment - switched to HTTP.  Looks like its related to TLS 1.2 (though it was still failing with min version 1.0?)
- Function app requires a content (posting nothing gives HTTP 411 - Length Required)
- Load test ignores the Content-Length header (so using Content-Length:0 is ignored) - solution is to pass a a raw payload typed as application/json of {}.
- Disable webjobs ref: https://blogs.msdn.microsoft.com/appserviceteam/2017/09/19/processing-100000-events-per-second-on-azure-functions/

- Test Run 12 -> Plain
- Test Run 13 - > No webjobs dashboard
- Test Run 14 -> add appinsights
- Test Run 16 - > EH