# AppLauncher

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg?style=for-the-badge)](LICENSE)

## How to Use

1. Publish your application for all target architectures.
2. Group them into a single directory using the specific architecture names.
3. Place the `AppLauncher` executable in the root of that directory.

### Target Directory Structure

Your deployment folder should look like this:

```text
your-app/
├── AppLauncher.exe          # (or AppLauncher on Linux)
├── win-x64/
│   └── your-app.exe
├── win-arm64/
│   └── your-app.exe
├── linux-x64/
│   └── your-app
└── linux-arm64/
    └── your-app
```

When a user runs `AppLauncher`, it detects the host operating system and architecture, then seamlessly routes the execution to the correct subfolder.

## Troubleshooting

### Linux: Permission Denied

If you encounter a `Permission Denied` error when running `AppLauncher` or your application on Linux, the execution bits might be missing. 

Fix this by granting executable permissions via the terminal:

```bash
# Make the AppLauncher executable
chmod +x ./AppLauncher

# Ensure the architecture-specific binaries are executable as well
chmod +x ./linux-x64/your-app
chmod +x ./linux-arm64/your-app
```

## Package Feeds

Default by `NuGet.config` is myget.org

| Feed                           | Feed Url                                                         |
| :----------------------------- | :--------------------------------------------------------------- |
| ![myget.org][myGetBadge]       | <https://www.myget.org/F/evilbaschdi/api/v3/index.json>          |
| ![codeberg.org][codebergBadge] | <https://codeberg.org/api/packages/evilbaschdi/nuget/index.json> |

## Quality & Activity

| Branch                                | Status & Activity                                                                                                                                                        |
| :------------------------------------ | :----------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| ![Main Branch][mainBranchBadge]       | [![CodeFactor][codeFactorMainBadge]][codeFactorMainOverview] ![Commit Activity Main][commitActivityMainBadge] ![Last Commit Main][lastCommitMainBadge]                   |
| ![Develop Branch][developBranchBadge] | [![CodeFactor][codeFactorDevelopBadge]][codeFactorDevelopOverview] ![Commit Activity Develop][commitActivityDevelopBadge] ![Last Commit Develop][lastCommitDevelopBadge] |

[myGetBadge]: https://img.shields.io/badge/MyGet.org-gray?style=for-the-badge&logo=myget
[codebergBadge]: https://img.shields.io/badge/Codeberg-gray?style=for-the-badge&logo=codeberg

[mainBranchBadge]: https://img.shields.io/badge/branch-main-brightgreen?style=for-the-badge&logo=git&logoColor=white&color=c9ff00
[developBranchBadge]: https://img.shields.io/badge/branch-develop-blue?style=for-the-badge&logo=git&logoColor=white&color=0080ff

[codeFactorMainBadge]: https://www.codefactor.io/repository/github/evilbaschdi/AppLauncher/badge/main?style=for-the-badge
[codeFactorMainOverview]: https://www.codefactor.io/repository/github/evilbaschdi/AppLauncher/overview/main
[commitActivityMainBadge]: https://img.shields.io/github/commit-activity/m/evilbaschdi/AppLauncher/main?style=for-the-badge
[lastCommitMainBadge]: https://img.shields.io/github/last-commit/evilbaschdi/AppLauncher/main?style=for-the-badge

[codeFactorDevelopBadge]: https://www.codefactor.io/repository/github/evilbaschdi/AppLauncher/badge/develop?style=for-the-badge
[codeFactorDevelopOverview]: https://www.codefactor.io/repository/github/evilbaschdi/AppLauncher/overview/develop
[commitActivityDevelopBadge]: https://img.shields.io/github/commit-activity/m/evilbaschdi/AppLauncher/develop?style=for-the-badge
[lastCommitDevelopBadge]: https://img.shields.io/github/last-commit/evilbaschdi/AppLauncher/develop?style=for-the-badge