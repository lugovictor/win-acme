﻿using ACMESharp.ACME;
using System.IO;

namespace LetsEncrypt.ACME.Simple.Plugins.ValidationPlugins.Http
{
    class FileSystemIIS : FileSystem
    {
        public override string Name
        {
            get
            {
                return "Http-FileSystem-IIS";
            }
        }

        public override void BeforeAuthorize(Options options, Target target, HttpChallenge challenge)
        {
            var x = new IISPlugin();
            x.UnlockSection("system.webServer/handlers");
            Program.Log.Debug("Writing web.config");
            WriteFile(CombinePath(target.WebRootPath, challenge.FilePath.Replace(challenge.Token, "web.config")), File.ReadAllText(_templateWebConfig));
        }

        public override void BeforeDelete(Options options, Target target, HttpChallenge challenge)
        {
            Program.Log.Debug("Deleting web.config");
            DeleteFile(CombinePath(target.WebRootPath, challenge.FilePath.Replace(challenge.Token, "web.config")));
        }
    }
}
