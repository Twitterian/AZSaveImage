using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Net;
using System.IO;

namespace AZSaveImage
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Script로부터 전달받은 트윗 객체 전체를 넘겨받습니다
            // 파싱을 위해 정규식을 만듭니다.
            var regx = new Regex(@"media_url:http:\/\/[^\/]+\/media\/[^.]+.[a-z]+");

            if (args.Length != 0)
            {
                // 이미지 저장 경로를 지정할 다이얼로그입니다.
                var savefile = new SaveFileDialog();
                savefile.InitialDirectory = Directory.GetCurrentDirectory();
                savefile.Title = "이미지 저장";
                savefile.Filter = "이미지 파일|*.*";

                // 전달받은 트윗 객체에 미디어가 4개 미만이어도 항상 링크는 4개 돌려받습니다.
                // 의도는 알 수 없지만, 같은 이미지를 여러번 저장하는 것은 무의미하기 때문에 여기선 파일 이름이 같다면 다이얼로그를 띄우지 않도록 합니다.
                var list = new List<string>();

                var match = regx.Match(args[0]); // 첫번째 매치 반환

                while (match.Success)
                {
                    var url = match.Value.Replace("media_url:", ""); 
                    var image = GetImageFromUrl(url); // url로부터 이미지 스트림을 얻어옵니다.
                    savefile.FileName = url.Replace("http://pbs.twimg.com/media/", ""); // url에서 끝 부분만 잘라내 파일 이름의 기본값으로 설정합니다.
                    if (list.Contains(savefile.FileName)) // 해당 값이 이미 리스트에 있다면 이 이미지에 대한 저장작업은 한번 이상 수행되었을 것임으로 다음 매치를 찾습니다.
                    {
                        match = match.NextMatch();
                        continue;
                    }
                    list.Add(savefile.FileName); // 이미지 파일 이름을 리스트에 추가합니다.
                    if (savefile.ShowDialog() == DialogResult.OK) 
                    {
                        image.Save(savefile.FileName); // 이미지 저장
                    }
                    match = match.NextMatch();
                }

            }
        }

        public static Image GetImageFromUrl(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                using (Stream stream = httpWebReponse.GetResponseStream())
                {
                    return Image.FromStream(stream);
                }
            }
        }
    }
}
