<# Copyright © 2020 Jeremy Herbison

This file is part of AudioWorks.

AudioWorks is free software: you can redistribute it and/or modify it under the terms of the GNU Affero General Public
License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later
version.

AudioWorks is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Affero General Public License for more
details.

You should have received a copy of the GNU Affero General Public License along with AudioWorks. If not, see
<https://www.gnu.org/licenses/>. #>

$localFeedDir = Join-Path -Path $([System.Environment]::GetFolderPath(28)) -ChildPath AudioWorks | Join-Path -ChildPath LocalFeed
$settingsFile = Join-Path -Path $([System.Environment]::GetFolderPath(26)) -ChildPath AudioWorks | Join-Path -ChildPath settings.json

$content = Get-Content $settingsFile | ConvertFrom-Json
$content.ExtensionRepository = $localFeedDir
$content | ConvertTo-Json | Set-Content -Path $settingsFile