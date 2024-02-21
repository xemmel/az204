

### OAUTH2 Policies


```xml

        <validate-jwt header-name="Authorization" failed-validation-httpcode="401" failed-validation-error-message="Unauthorized. Access token is missing or invalid.">
            <openid-config url="https://login.microsoftonline.com/adminintegrationit.onmicrosoft.com/.well-known/openid-configuration" />
            <audiences>
                <audience>api://3e1b9cb8-9f7d-40f3-aacb-562c4b2a5210</audience>
            </audiences>
            <required-claims>
                <claim name="roles" match="all">
                    <value>writer</value>
                </claim>
            </required-claims>
        </validate-jwt>
        <authentication-managed-identity resource="https://storage.azure.com/" />


```