using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fileDel
{
    public class ListFileInfo
    {
        public readonly FileInfo File;

        private List <string>? mItems = null;

        public List <string> Items
        {
            get
            {
                if (mItems == null)
                    Load ();

                return mItems!;
            }
        }

        public ListFileInfo (string path)
        {
            if (Path.IsPathFullyQualified (path) == false)
                throw new ArgumentException ();

            File = new FileInfo (path);
        }

        public void Load ()
        {
            mItems = new List <string> ();

            if (File.Exists)
            {
                // 行末の空白や空行を無視し、パスとしての正当性をチェックせず、内容に重複のないリストを取得
                // このクラスにおいて項目の大文字・小文字が区別されないのは決め打ちの仕様

                Items.AddRange (System.IO.File.ReadAllLines (File.FullName, Encoding.UTF8).
                    Select (x => x.TrimEnd ()).
                    Where (x => x.Length > 0).
                    Distinct (StringComparer.OrdinalIgnoreCase));
            }
        }

        public void Save ()
        {
            // リストの手作業での編集も想定し、項目を見付けやすいようにソート
            Items.Sort (StringComparer.OrdinalIgnoreCase);

            if (Directory.Exists (File.DirectoryName) == false)
                Directory.CreateDirectory (File.DirectoryName!);

            System.IO.File.WriteAllLines (File.FullName, Items, Encoding.UTF8);
        }
    }
}
