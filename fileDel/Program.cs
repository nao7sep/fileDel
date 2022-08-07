namespace fileDel
{
    internal class Program
    {
        static void Main (string [] args)
        {
            try
            {
                int xErrorCount = 0;
                List <string> xPaths = new List <string> ();

                foreach (string xArg in args)
                {
                    string xTrimmed = xArg.Trim ();

                    if (xTrimmed.Length > 0)
                    {
                        if (Path.IsPathFullyQualified (xTrimmed) == false)
                        {
                            xErrorCount ++;
                            Shared.WriteStrongLineToConsole ("不正なパスです: " + xTrimmed);
                        }

                        else
                        {
                            // ディレクトリーやファイルの存在をチェックしない

                            if (xPaths.Contains (xTrimmed, StringComparer.OrdinalIgnoreCase) == false)
                                xPaths.Add (xTrimmed);
                        }
                    }
                }

                if (xErrorCount > 0)
                    return; // finally へ

                ListFileInfo xList = new ListFileInfo (iShared.TargetPathsListFilePath);

                // まだパスがなければ初回起動とみなす

                if (xList.Items.Count == 0 && xPaths.Count == 0)
                {
                    Shared.WriteStrongLineToConsole ("削除するディレクトリーやファイルをプログラムの実行ファイルにドラッグ＆ドロップしてください。");
                    return;
                }

                xList.Items.AddRange (xPaths.
                    Where (x => xList.Items.Contains (x, StringComparer.OrdinalIgnoreCase) == false).
                    Distinct (StringComparer.OrdinalIgnoreCase));

                foreach (string xPath in xList.Items)
                {
                    // 読み取り専用などにより削除に失敗することがあるため属性を外す

                    if (Directory.Exists (xPath))
                    {
                        try
                        {
                            File.SetAttributes (xPath, FileAttributes.Directory);
                            Directory.Delete (xPath, recursive: true);
                            Console.WriteLine ("ディレクトリーを削除しました: " + xPath);
                        }

                        catch
                        {
                            Shared.WriteStrongLineToConsole ("ディレクトリーを削除できません: " + xPath);
                        }
                    }

                    else if (File.Exists (xPath))
                    {
                        try
                        {
                            File.SetAttributes (xPath, FileAttributes.Normal);
                            File.Delete (xPath);
                            Console.WriteLine ("ファイルを削除しました: " + xPath);
                        }

                        catch
                        {
                            Shared.WriteStrongLineToConsole ("ファイルを削除できません: " + xPath);
                        }
                    }
                }

                xList.Save ();
            }

            catch (Exception xException)
            {
                Shared.WriteStrongLineToConsole ("エラーが発生しました:");
                Shared.WriteStrongLineToConsole (xException.ToString ().TrimEnd ());
            }

            finally
            {
                Console.Write ("プログラムを終了するには、任意のキーを押してください: ");
                Console.ReadKey (true);
                Console.WriteLine ();
            }
        }
    }
}
