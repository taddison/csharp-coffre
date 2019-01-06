# Test Plan

## Build and deploy
  [x] Plain function app
  [ ] Plain function app with no webjobs dashboard
  [ ] Plain function app with no webjobs dashboard, add app insights
  [ ] Function app with EH output, fastest from above

## Test
  [ ] Plain
  [ ] Plain no webjobs
  [ ] Plain appinsights
  [ ] EH

## Notes
- Azure Devops load test fails when trying to target default deployment - switched to HTTP.  Looks like its related to TLS 1.2 (though it was still failing with min version 1.0?)
- Function app requires a content (posting nothing gives HTTP 411 - Length Required)
- Load test ignores the Content-Length header (so using Content-Length:0 is ignored) - solution is to pass a a raw payload typed as application/json of {}.