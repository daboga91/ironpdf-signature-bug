# IronPdf Digital Signature Bug
Given a valid certificate chain, with a self signed root certificate:

Test_Root.cert.pem ---> Test_Intermediate.cert.pem ---> Test_Digital_Signature.cert.pem.

IronPdf refuses to digitally sign the pdf document, unless Test_Intermediate.cert.pem is added to Windows Users' or Local Machine's trusted store, or there is no certificate chain only one certificate issued by itself.

Nor addig Test_Digital_Signature.cert.pem or Test_Root.cert.pem resolves the problem, only adding the intermediate CA makes digital signature work.
The solution contains 3 private key pem files, 3 certificate pem files, a certificate chain pem file, and a generated pkcs12 file of which both are tested in the above solution.

The private keys are in clear text.

The pkcs12 file was generated using this command:
```
openssl pkcs12 -inkey private/Test_Digital_Signiture.key.pem -in chain.cert.pem -export -out chain.pfx
```

The last known version where signature worked was IronPdf 2022.7.6986

## Expected behavior
There is no certificate validation during signing a pdf, since it is the responsibility of the party receiving it to check the source.

## Actual behavior
PdfDocument.Sign function tries to validate the signing certificate's issuer, and goes no further up the chain (is this required by the pdf standard or anything?).

## Reproduction steps

1. Clone https://github.com/daboga91/ironpdf-signature-bug.git
2. Build and run IronPdfSignatureBug
3. Signing fails
4. Import Test_Intermediate.cert.pem to the current user's trusted or personal certificate store
5. Run IronPdfSignatureBug
6. Signing completes
7. Delete Test_Intermediate.cert.pem from the store
8. Import Test_Root.cert.pem or Test_Digital_Signature.cert.pem to the trusted store
9. Run IronPdfSignatureBug
10. Signing fails

## Configuration
**TargetFramework:** dotnet-sdk 8.0.301-win-x64

**OS version:** Microsoft Windows 10 Enterprise 10.0.19044

**IronPdf nuget version:** 2024.5.2
