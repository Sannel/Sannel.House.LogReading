ARG tag=2.2

FROM sannel/house.openfaasrunner:${tag} AS build
FROM build AS base
COPY --chown=house:house Function/* /app/
COPY --from=build /app /app