# Panic

.net 10 test project

## Podman

Build
```bash
podman build --no-cache -t panic .
```
Run
```bash
podman run --network host --rm localhost/panic -p 8080:8080
```

## Central Package Management

- [Central Package Management (CPM)](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)
- [CPM Converter](https://github.com/Webreaper/CentralisedPackageConverter)