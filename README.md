Run KeyCloak in Docker using the following command :

docker run --name keycloak -p 8080:8080 -e KEYCLOAK_ADMIN=admin -e KEYCLOAK_ADMIN_PASSWORD=admin quay.io/keycloak/keycloak start-dev

Given the realm is master, then the keycloak endpoint to get JWT is 
http://localhost:8080/realms/master/protocol/openid-connect/token

Send Payload for the keycloak endpoint as

username : "emailid"
password : ""
grant_type: "password"
client_id: ""
realms: "master"
