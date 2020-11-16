﻿<# Copyright © 2020 Jeremy Herbison

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
# SIG # Begin signature block
# MIIVSAYJKoZIhvcNAQcCoIIVOTCCFTUCAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCCuihgWnDMxc5LY
# smMwHbWY+A6Inc+kV9ZrULJZpD0hvKCCDwowggTcMIIDxKADAgECAhEA/mfk8Vok
# 48YNVHygIMJ2cDANBgkqhkiG9w0BAQsFADB+MQswCQYDVQQGEwJQTDEiMCAGA1UE
# ChMZVW5pemV0byBUZWNobm9sb2dpZXMgUy5BLjEnMCUGA1UECxMeQ2VydHVtIENl
# cnRpZmljYXRpb24gQXV0aG9yaXR5MSIwIAYDVQQDExlDZXJ0dW0gVHJ1c3RlZCBO
# ZXR3b3JrIENBMB4XDTE2MDMwODEzMTA0M1oXDTI3MDUzMDEzMTA0M1owdzELMAkG
# A1UEBhMCUEwxIjAgBgNVBAoMGVVuaXpldG8gVGVjaG5vbG9naWVzIFMuQS4xJzAl
# BgNVBAsMHkNlcnR1bSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTEbMBkGA1UEAwwS
# Q2VydHVtIEVWIFRTQSBTSEEyMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKC
# AQEAv1eLvJEzWi5XMX8lV/RbU1hqJarogSDMDR1HOHAaoxY3nbdEdLUagST69RhK
# OEsLoLrFvzRv6oz1nUIa0DGoVt2oJQ60PCXFrMbLXOOAkuqjry0AQEB80kEoHysI
# 6FHQXYlwImxpdtB2EjwuSwcpJun4AeHQ5Sj2JMMV+qaQhHSFXIMsDsTaeEmUah0k
# hpfpIsDGDDXgdDKqPbsB2H7ME0wgx5UtSfbxLRe8xin3+FV2nH0V3N7hQpWTYJn3
# Q8WUQiG9mKwcs2bc/XhgRD89xJVpZ+5hy9rQueZ296E/BPTT53GvIQJeEdpTpKa1
# kXjZkBFbtKHup24K2XOkOAVSIwIDAQABo4IBWjCCAVYwDAYDVR0TAQH/BAIwADAd
# BgNVHQ4EFgQU8zXKjkYIDTmN30HHM25k5BY7mCswHwYDVR0jBBgwFoAUCHbNywf/
# JPbFze27kLzihDdGdfcwDgYDVR0PAQH/BAQDAgeAMBYGA1UdJQEB/wQMMAoGCCsG
# AQUFBwMIMC8GA1UdHwQoMCYwJKAioCCGHmh0dHA6Ly9jcmwuY2VydHVtLnBsL2N0
# bmNhLmNybDBrBggrBgEFBQcBAQRfMF0wKAYIKwYBBQUHMAGGHGh0dHA6Ly9zdWJj
# YS5vY3NwLWNlcnR1bS5jb20wMQYIKwYBBQUHMAKGJWh0dHA6Ly9yZXBvc2l0b3J5
# LmNlcnR1bS5wbC9jdG5jYS5jZXIwQAYDVR0gBDkwNzA1BgsqhGgBhvZ3AgUBCzAm
# MCQGCCsGAQUFBwIBFhhodHRwOi8vd3d3LmNlcnR1bS5wbC9DUFMwDQYJKoZIhvcN
# AQELBQADggEBAMp05Di9MskaPPorWMVXLTVTC5DeLQWy8TMyQBuW/yJFhzmuDPAZ
# zsHQMkQaMwyA6z0zK3x5NE7GgUQ0WFa6OQ3w5LMDrDd1wHrrt0D2mvx+gG2ptFWJ
# PZhIylb0VaQu6eHTfrU4kZXEz7umHnVrVlCbbqfr0ZzhcSDV1aZYq+HlKV2B8QS1
# 5BtkQqE4cT17c2TGadQiMJawJMMCWxGoPDRie2dn4UaGV3zoip+Quzhb2bWJ6gMo
# 2423WwdtMruHf9wmzi5e6Nar2+am0OIZAhL5oNs+nVLETL1Xhe147cGWRM1GsM5l
# 1VdyOiTGEOGwc8SPWoOs9sZylPlyd/8B1SEwggTeMIIDxqADAgECAhBrMmoPAyjT
# eh1TC/0jvUjiMA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlBMMSIwIAYDVQQK
# ExlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYDVQQLEx5DZXJ0dW0gQ2Vy
# dGlmaWNhdGlvbiBBdXRob3JpdHkxIjAgBgNVBAMTGUNlcnR1bSBUcnVzdGVkIE5l
# dHdvcmsgQ0EwHhcNMTUxMDI5MTEzMDI5WhcNMjcwNjA5MTEzMDI5WjCBgDELMAkG
# A1UEBhMCUEwxIjAgBgNVBAoMGVVuaXpldG8gVGVjaG5vbG9naWVzIFMuQS4xJzAl
# BgNVBAsMHkNlcnR1bSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTEkMCIGA1UEAwwb
# Q2VydHVtIENvZGUgU2lnbmluZyBDQSBTSEEyMIIBIjANBgkqhkiG9w0BAQEFAAOC
# AQ8AMIIBCgKCAQEAt9uo2MjjvNrag7q5v9bVV1NBt0C6FwxEldTpZjt/tL6Qo5QJ
# pa0hIBeARrRDJj6OSxpk7A5AMkP8gp//Si3qlN1aETaLYe/sFtRJA9jnXcNlW/JO
# CyvDwVP6QC3CqzMkBYFwfsiHTJ/RgMIYew4UvU4DQ8soSLAt5jbfGz2Lw4ydN57h
# BtclUN95Pdq3X+tGvnYoNrgCAEYD0DQbeLQox1HHyJU/bo2JGNxJ8cIPGvSBgcdt
# 1AR3xSGjLlP5d8/cqZvDweXVZy8xvMDCaJxKluUf8fNINQ725LHF74eAOuKADDSd
# +hRkceQcoaqyzwCn4zdy+UCtniiVAg3OkONbxQIDAQABo4IBUzCCAU8wDwYDVR0T
# AQH/BAUwAwEB/zAdBgNVHQ4EFgQUwHu0yLduVqcJSJr4ck/X1yQsNj4wHwYDVR0j
# BBgwFoAUCHbNywf/JPbFze27kLzihDdGdfcwDgYDVR0PAQH/BAQDAgEGMBMGA1Ud
# JQQMMAoGCCsGAQUFBwMDMC8GA1UdHwQoMCYwJKAioCCGHmh0dHA6Ly9jcmwuY2Vy
# dHVtLnBsL2N0bmNhLmNybDBrBggrBgEFBQcBAQRfMF0wKAYIKwYBBQUHMAGGHGh0
# dHA6Ly9zdWJjYS5vY3NwLWNlcnR1bS5jb20wMQYIKwYBBQUHMAKGJWh0dHA6Ly9y
# ZXBvc2l0b3J5LmNlcnR1bS5wbC9jdG5jYS5jZXIwOQYDVR0gBDIwMDAuBgRVHSAA
# MCYwJAYIKwYBBQUHAgEWGGh0dHA6Ly93d3cuY2VydHVtLnBsL0NQUzANBgkqhkiG
# 9w0BAQsFAAOCAQEAquU/dlQCTHAOKak5lgYPMbcL8aaLUvsQj09CW4y9MSMBZp3o
# KaFNw1D69/hFDh2C1/z+pjIEc/1x7MyID6OSCMWBWAL9C2k7zbg/ST3QjRwTFGgu
# mw2arbAZ4p7SfDl3iG8j/XuE/ERttbprcJJVbJSx2Df9qVkdtGOy3BPNeI4lNcGa
# jzeELtRFzOP1zI1zqOM6beeVlHBXkVC2be9zck8vAodg4uoioe0+/dGLZo0ucm1P
# xl017pOomNJnaunaGc0Cg/l0/F96GAQoHt0iMzt2bEcFXdVS/g66dvODEMduMF+n
# YMf6dCcxmyiD7SGKG/EjUoTtlbytOqWjQgGdvDCCBUQwggQsoAMCAQICEAiJRXNz
# I/QuBYKVrqDJ26swDQYJKoZIhvcNAQELBQAwgYAxCzAJBgNVBAYTAlBMMSIwIAYD
# VQQKDBlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYDVQQLDB5DZXJ0dW0g
# Q2VydGlmaWNhdGlvbiBBdXRob3JpdHkxJDAiBgNVBAMMG0NlcnR1bSBDb2RlIFNp
# Z25pbmcgQ0EgU0hBMjAeFw0yMDA3MDcwOTA5MzRaFw0yMTA3MDcwOTA5MzRaMIGs
# MQswCQYDVQQGEwJDQTEQMA4GA1UECAwHQWxiZXJ0YTEQMA4GA1UEBwwHQ2FsZ2Fy
# eTEeMBwGA1UECgwVT3BlbiBTb3VyY2UgRGV2ZWxvcGVyMS8wLQYDVQQDDCZPcGVu
# IFNvdXJjZSBEZXZlbG9wZXIsIEplcmVteSBIZXJiaXNvbjEoMCYGCSqGSIb3DQEJ
# ARYZamVyZW15LmhlcmJpc29uQGdtYWlsLmNvbTCCASIwDQYJKoZIhvcNAQEBBQAD
# ggEPADCCAQoCggEBAKtcpDZnwp1bBZdmMc7UuYB/2MwRDela4PgXWEYFfxXLiDtU
# qO8yDL60EUk46HYKbnQ/46VedOT/XHejJu15kMxCNvGBlqpcAvfWWR5CmxZEfQ0k
# MwS6QsY0IqH6G36WJhIaipN26SV51qaiqZJ+C39nynMWMrUKDUaVYhAJPGJw3AxH
# 7Okcg4KQpoRp17iopdkTd6tlXXUdBU34zK+4K/SNLboOGwReOHXX7QPTt55PRCps
# bPBtmRZUYjlu4GF/czZgmrYtt2KD/vNGOJeV64lgsDXOGhLiZf9B7uLSuBgJ9uUd
# hvi/bUd78VHzeSdlqPAv1SE9U3MaXEL6HrYriNsCAwEAAaOCAYowggGGMAwGA1Ud
# EwEB/wQCMAAwMgYDVR0fBCswKTAnoCWgI4YhaHR0cDovL2NybC5jZXJ0dW0ucGwv
# Y3NjYXNoYTIuY3JsMHEGCCsGAQUFBwEBBGUwYzArBggrBgEFBQcwAYYfaHR0cDov
# L2NzY2FzaGEyLm9jc3AtY2VydHVtLmNvbTA0BggrBgEFBQcwAoYoaHR0cDovL3Jl
# cG9zaXRvcnkuY2VydHVtLnBsL2NzY2FzaGEyLmNlcjAfBgNVHSMEGDAWgBTAe7TI
# t25WpwlImvhyT9fXJCw2PjAdBgNVHQ4EFgQUa7EeFY9eeL0Q95FJ1iWELbpapY8w
# HQYDVR0SBBYwFIESY3NjYXNoYTJAY2VydHVtLnBsMA4GA1UdDwEB/wQEAwIHgDBL
# BgNVHSAERDBCMAgGBmeBDAEEATA2BgsqhGgBhvZ3AgUBBDAnMCUGCCsGAQUFBwIB
# FhlodHRwczovL3d3dy5jZXJ0dW0ucGwvQ1BTMBMGA1UdJQQMMAoGCCsGAQUFBwMD
# MA0GCSqGSIb3DQEBCwUAA4IBAQCgRfcD/saeY2f4/4f4jEqLcBF1icVnR1L1yFn5
# 8MPtT5I9RVsTk+WwkCFlu1jycykbebWENth7BEq4KMBg9YCSZJhRZl7kLh12G9Nk
# nOuoXOALOZpwzIcvVlOIvZTwj10r3QFcrgP+FefzjiquwY+O0/YFhf+WpkTfEDzU
# 3uA5FyYgJRwN8KAhv6yU3b8HtpS7MkKSPa2PtMId8YiCEw0PRBSsGX6pKPZ/c5e1
# JWKC1slneovG8pwwq9b49cKG7zuE/bRc0Fg/T5/QMFOPdUGjNwyO34Crpzq/2ZJV
# qEbz3jl0wcynfu1tJxeb9gjkzkOMPFAM/+HRcl5fH6WfIYNPMYIFlDCCBZACAQEw
# gZUwgYAxCzAJBgNVBAYTAlBMMSIwIAYDVQQKDBlVbml6ZXRvIFRlY2hub2xvZ2ll
# cyBTLkEuMScwJQYDVQQLDB5DZXJ0dW0gQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkx
# JDAiBgNVBAMMG0NlcnR1bSBDb2RlIFNpZ25pbmcgQ0EgU0hBMgIQCIlFc3Mj9C4F
# gpWuoMnbqzANBglghkgBZQMEAgEFAKCBhDAYBgorBgEEAYI3AgEMMQowCKACgACh
# AoAAMBkGCSqGSIb3DQEJAzEMBgorBgEEAYI3AgEEMBwGCisGAQQBgjcCAQsxDjAM
# BgorBgEEAYI3AgEVMC8GCSqGSIb3DQEJBDEiBCB37a/gCuqZfV+Fa1uudNhIxsv4
# DReIHFjtBtK/G0/qQjANBgkqhkiG9w0BAQEFAASCAQBeXbJ4Deg7FwTTmqdw7nOc
# IM3hABZHh2Y3JuEyFNDHX0gcjbyNhpYd3/2Rtk9kYmJ/2eaWy02fQZ+GeacC2tDi
# fB/VzH+09I2R4ZlHTBLk5ybQtF/Nr6/g/8zQo+8+m0r42j1ZakICIPu7nVcO/30U
# FXe4lphX4uUHPVUwmPozHRWqeahqKVYc42GjLcbw2JhMHC+i/LDh5tC9YLZ1TaYa
# nDvdpv9zfvQlANfmS+oGiWGV41A9Jdhxtz75X8Ec28g+OztTgwRF/KU1ppRMJGHa
# 7B3hIeUO88+newhXWdlPGz+DF0Fx1yc8+a0E4SYaNSloR63hqc7wNXVh5FvF3s6p
# oYIDSDCCA0QGCSqGSIb3DQEJBjGCAzUwggMxAgEBMIGTMH4xCzAJBgNVBAYTAlBM
# MSIwIAYDVQQKExlVbml6ZXRvIFRlY2hub2xvZ2llcyBTLkEuMScwJQYDVQQLEx5D
# ZXJ0dW0gQ2VydGlmaWNhdGlvbiBBdXRob3JpdHkxIjAgBgNVBAMTGUNlcnR1bSBU
# cnVzdGVkIE5ldHdvcmsgQ0ECEQD+Z+TxWiTjxg1UfKAgwnZwMA0GCWCGSAFlAwQC
# AQUAoIIBcjAaBgkqhkiG9w0BCQMxDQYLKoZIhvcNAQkQAQQwHAYJKoZIhvcNAQkF
# MQ8XDTIwMTExNjIzNDI0MFowLwYJKoZIhvcNAQkEMSIEIL//S4e/IStv4VJ6AJZi
# 6MAHqAi468/SiUgMbztbBz80MDcGCyqGSIb3DQEJEAIvMSgwJjAkMCIEINnKq8Mi
# W3Awzbg+OEbjaRwU7XkLF2TOG08EMVeJnFxYMIHLBgsqhkiG9w0BCRACDDGBuzCB
# uDCBtTCBsgQUT41MSAZJQmrvi4bU1fx5MucULYUwgZkwgYOkgYAwfjELMAkGA1UE
# BhMCUEwxIjAgBgNVBAoTGVVuaXpldG8gVGVjaG5vbG9naWVzIFMuQS4xJzAlBgNV
# BAsTHkNlcnR1bSBDZXJ0aWZpY2F0aW9uIEF1dGhvcml0eTEiMCAGA1UEAxMZQ2Vy
# dHVtIFRydXN0ZWQgTmV0d29yayBDQQIRAP5n5PFaJOPGDVR8oCDCdnAwDQYJKoZI
# hvcNAQEBBQAEggEAk4rEu5TxLwDoFbIqAK4ZbSj8BEnnkbBj/EAqJxVedd3TG8lz
# 0KREeXMvyx74RfxnYzMZMJdcYYzKiAfG3aaN7BqwKnPL7KGuASThrCLrEkIF0/vu
# osrZsii3Fnoqu5C465RNHR/QLkHfQvhtUaodLeUohzdSaoQZr+QzqVn0GbsbYHxy
# 5G9d7ha778EBzQcXXg0kTZUkwmTlX9iWToMB1MB865ZJSb3fbAhNK2NbluCERIuh
# sz0uyS0lGCmLp+LInZWP5uWnTKbQ2VInd3L/bclYZsVul8FkaGgMf5ZTe7t5GO0A
# VdJYreM4v1L3ZHjS284EEuUKge7hF9VyPof03g==
# SIG # End signature block
