@echo off
for /f %%x in ('wmic path win32_utctime get /format:list ^| findstr "="') do set %%x
set Month=0%Month%
set Month=%Month:~-2%
set Day=0%Day%
set Day=%Day:~-2%
set Hour=0%Hour%
set Hour=%Hour:~-2%
set Minute=0%Minute%
set Minute=%Minute:~-2%
set Second=0%Second%
set Second=%Second:~-2%
set Timestamp=%Year%.%Month%.%Day%.%Hour%.%Minute%.%Second%
@echo namespace AvitoRuslanParser > BuildTimeStamp.cs
@echo { >> BuildTimeStamp.cs
@echo   class BuildTimeStamp >> BuildTimeStamp.cs
@echo   { >> BuildTimeStamp.cs
@echo     public static string TimeStamp() >> BuildTimeStamp.cs
@echo     { >> BuildTimeStamp.cs
@echo       return "%Timestamp%"; >> BuildTimeStamp.cs
@echo     } >> BuildTimeStamp.cs
@echo   } >> BuildTimeStamp.cs
@echo } >> BuildTimeStamp.cs
