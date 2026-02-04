FROM mcr.microsoft.com/devcontainers/dotnet:9.0

# Libraries Avalonia often needs on Linux
RUN apt-get update && apt-get install -y \
    libx11-6 libx11-xcb1 libxrender1 libxi6 libxtst6 libxrandr2 libxcursor1 \
    libxcomposite1 libxdamage1 libxfixes3 libglib2.0-0 libfontconfig1 libfreetype6 \
    && rm -rf /var/lib/apt/lists/*
