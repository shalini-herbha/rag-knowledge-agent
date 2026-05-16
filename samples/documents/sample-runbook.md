# Sample Support Runbook

This is a sample document used for testing the RAG Knowledge Agent.

## Scenario

A customer reports that search results are not loading.

## Initial Checks

1. Confirm the API is reachable.
2. Check whether the search service is returning errors.
3. Review recent deployment changes.
4. Check logs for timeout or validation errors.

## Suggested Response

If the search API is unavailable, return a friendly error message and ask the user to try again shortly.

## Escalation

Escalate to the engineering team if the issue affects multiple users or continues for more than 15 minutes.
