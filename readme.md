# JWT Authentication with Custom Certificates

## 📌 Steps to Run the Application

1. **Configure Certificates**: Use a custom certificate path in `appSettings.json` or rely on default certificates.
2. **Run the Applications**: Start `JwtCert.Issuer` and `JwtCert.Api`.
3. **Generate a JWT Token**: Obtain a token from the `Issuer` application.
4. **Authorize API Requests**: Use the generated token to authenticate API requests.

---

## 🔑 Generate Private Certificate with .NET Dev-Certs

```sh
dotnet dev-certs https -ep cert.pfx -p Senha123
```

### ✅ Add Certificate to Trusted Store (Optional)
```sh
dotnet dev-certs https --trust
```

---

## 🔓 Generate Public Certificate

```sh
openssl pkcs12 -in cert.pfx -clcerts -nokeys -out public_cert.cer -passin pass:Senha123
```

This extracts the public key for verification purposes.

---