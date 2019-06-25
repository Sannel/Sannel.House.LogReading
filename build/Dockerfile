ARG tag=2.2

FROM sannel/house.openfaasrunner:${tag} AS base
FROM base as build
COPY Function/* /app/
COPY --from=base /app/ /app/
COPY Function/Function.dll /app/
