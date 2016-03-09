using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nihongo.Dal.Dao;
using Nihongo.Models;
using Ivs.Core.Data;
using Ivs.Core.Common;
using Ivs.Core.Web.Attributes;

namespace Nihongo.Controllers
{
    public class TestController : BaseController
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult Hiragana()
        {
            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByNone")]
        public ActionResult Katakana()
        {
            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByID")]
        public ActionResult Vocabulary(string id)
        {
            ViewBag.LessonCode = id;

            //int returnCode = 0;
            //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            //model.LessonCode = id;
            //if (CommonMethod.IsNullOrEmpty(UserSession.UserName))
            //{
            //    returnCode = vocaDao.SelectDataForLesson(model, out results);
            //}
            //else
            //{
            //    returnCode = vocaDao.SelectVocaForLesson(model, out results);
            //}

            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByID")]
        public ActionResult WeakVocabulary(string id)
        {
            ViewBag.LessonCode = id;

            //List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            //model.LessonCode = id;
            //model.UserName = UserSession.UserName;
            //int returnCode = vocaDao.SelectWeakVocaForLesson(model, out results);

            return View();
        }

        [OutputCache(CacheProfile = "Cache1HourVaryByID")]
        public ActionResult LearntVocabulary(string id)
        {
            ViewBag.LessonCode = id;

            return View();
        }

        //[OutputCache(CacheProfile = "Cache1HourVaryByID")]
        public ActionResult Conversation(string id)
        {
            return View();
        }

        //public ActionResult GetConversationDetails(string conversationCode)
        //{
        //    //select conversatioin
        //    List<MS_ConversationsModels> conversations = new List<MS_ConversationsModels>();
        //    MS_ConversationsModels conModel = new MS_ConversationsModels();
        //    conModel.Code = conversationCode;
        //    int returnCode = (new MS_ConversationsDao()).SelectData(conModel, out conversations);

        //    //select conversation details
        //    List<MS_ConversationDetailsModels> details = new List<MS_ConversationDetailsModels>();
        //    MS_ConversationDetailsModels condition = new MS_ConversationDetailsModels();
        //    condition.ConversationCode = conversationCode;
        //    returnCode = (new MS_ConversationsDao()).SelectDetailData(condition, out details);

        //    return Json(new
        //    {
        //        conversation = conversations.Count > 0 ? conversations[0] : new MS_ConversationsModels(),
        //        conversationDetails = details
        //    }, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetTest(string lessonCode)
        {
            ////select conversatioin
            //List<MS_ConversationsModels> conversations = new List<MS_ConversationsModels>();
            //MS_ConversationsModels conModel = new MS_ConversationsModels();
            //conModel.Code = conversationCode;
            //int returnCode = (new MS_ConversationsDao()).SelectData(conModel, out conversations);


            ////select conversation details
            List<MS_ConversationDetailsModels> details = new List<MS_ConversationDetailsModels>()
            {
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-1.mp3", Japanese = "ミラーさん　は　学生　ですか。", Romaji = "Milaa-san ha gakusei desuka.", Vietnamese = "Cô Milaa là sinh viên phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-2.mp3", Japanese = "ワンーさん　は　エンジニア　ですか。", Romaji = "Quan-san ha enjinia desuka.", Vietnamese = "Anh Quan là kĩ sư phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-3.mp3", Japanese = "あの方　は　どなた　ですか。", Romaji = "Anokata ha donata desuka.", Vietnamese = "Vị ấy là ai vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-4.mp3", Japanese = "テレサちゃん　は　何さい　ですか。", Romaji = "Teresa-chan ha nansai desuka.", Vietnamese = "Em Teresa bao nhiêu tuổi?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-5.mp3", Japanese = "マイクミラーさん　ですか。", Romaji = "Maikumiraa-san desuka.", Vietnamese = "Cô Maikumiraa phải không?" },

                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-6.mp3", Japanese = "しつれい　ですが。お名前　は。", Romaji = "Shitsurei desuga. Onamae ha.", Vietnamese = "Xin phép. Bạn tên gì?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-7.mp3", Japanese = "あなた　は　先生　ですか。", Romaji = "Anata ha sensei desuka.", Vietnamese = "Bạn là giáo viên phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-8.mp3", Japanese = "あなた　は　サんと-さん　ですか。", Romaji = "Anata ha Santosan desuka.", Vietnamese = "Bạn là anh Santo phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-9.mp3", Japanese = "何さい　ですか。", Romaji = "Nansai desuka.", Vietnamese = "Bao nhiêu tuổi?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-10.mp3", Japanese = "アメリカ人　ですか。", Romaji = "Amerikajin desuka.", Vietnamese = "Người Mỹ phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "1", UrlAudio = @"/Content/media/QA/1-11.mp3", Japanese = "エンジニア　ですか。", Romaji = "Enjinia desuka.", Vietnamese = "Kĩ sư phải không?" },

                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-1.mp3", Japanese = "これ　は　テレホン　カード　ですか。", Romaji = "Kore ha terehon kaado desuka.", Vietnamese = "Đây là card điện thoại phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-2.mp3", Japanese = "それ　は　ノート　ですか。", Romaji = "Sore ha nouto desuka.", Vietnamese = "Đó là quyển tập phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-3.mp3", Japanese = "それ　は　何　ですか。", Romaji = "Sore ha nan desuka.", Vietnamese = "Đó là gì?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-4.mp3", Japanese = "これ　は　９　ですか。７　ですか。", Romaji = "Kore ha 9 desu ka, 7 desuka.", Vietnamese = "Đây là 9 hay 7?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-5.mp3", Japanese = "それ　は　何　の　ざっし　ですか。", Romaji = "Sore ha nan no zasshi desuka.", Vietnamese = "Đó là tạp chí gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-6.mp3", Japanese = "あれ　は　だれ　の　かばん　ですか。", Romaji = "Are ha dare no kaban desuka.", Vietnamese = "Kia là cặp của ai?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-7.mp3", Japanese = "この　かさ　は　あなた　の　ですか。", Romaji = "Kono kasa ha anata no desuka.", Vietnamese = "Cái dù này của bạn phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-8.mp3", Japanese = "この　かぎ　は　だれ　の　ですか。", Romaji = "Kono kagi ha dare no desuka.", Vietnamese = "Chìa khóa này của ai vậy?" },

                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-9.mp3", Japanese = "これ　は　てちょう　ですか。", Romaji = "Kore ha techou desuka.", Vietnamese = "Đây là quyển tập phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-10.mp3", Japanese = "これ　は　コンピューター　ですか。テープ レコーダー　ですか。", Romaji = "Kore ha konpyuutaa desuka, teipu rekoudaa desuka", Vietnamese = "Đây là máy tính hay băng ghi âm?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-11.mp3", Japanese = "これ　は　何　ですか。", Romaji = "Kore ha nan desuka.", Vietnamese = "Đây là cái gì?" },
                new MS_ConversationDetailsModels(){ LessonCode = "2", UrlAudio = @"/Content/media/QA/2-12.mp3", Japanese = "この かばん　は　あなたの　ですか。", Romaji = "Kono kaban ha anata no desuka.", Vietnamese = "Cái cặp này của bạn phải không?" },
                
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-1.mp3", Japanese = "ここ　は　しんおおさか　ですか。", Romaji = "Koko ha shinoosaka desuka.", Vietnamese = "Đây là ga Osaka phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-2.mp3", Japanese = "おてあらい　は　どこ　ですか。", Romaji = "Otearai ha doko desuka.", Vietnamese = "Nhà vệ sinh ở đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-3.mp3", Japanese = "山田さん　は　どこ　ですか。", Romaji = "Yamada-san ha doko desuka.", Vietnamese = "Anh Yamada ở đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-4.mp3", Japanese = "エレベーター　は　どちら　ですか。", Romaji = "Erebeitaa ha dochira desuka.", Vietnamese = "Thang máy ở đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-5.mp3", Japanese = "（お）国　は　どちら　ですか。", Romaji = "(O)kuni ha dochira desuka.", Vietnamese = "Nước (quốc gia)(bạn) ở đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-6.mp3", Japanese = "それ　は　どこの　くつ　ですか。", Romaji = "Sore ha doko no kutsu desuka.", Vietnamese = "Đó là giày xuất xứ ở đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-7.mp3", Japanese = "この とけい　は　いくら　ですか。", Romaji = "Kono tokei ha ikura desuka.", Vietnamese = "Đồng hồ này bao nhiêu tiền vậy?" },
                
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-8.mp3", Japanese = "うち　は　どちら　ですか。", Romaji = "Uchi ha dochira desuka.", Vietnamese = "Nhà ở đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-9.mp3", Japanese = "すみません。ネクタイ　うりば　は　どこ　ですか。", Romaji = "Sumimasen, nekutai uriba ha doko desuka", Vietnamese = "Xin phép, quầy cà vạt ở đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-10.mp3", Japanese = "すみません。電話　は　どこ　ですか。", Romaji = "Sumimasen, denwa ha doko desuka.", Vietnamese = "Xin phép, điện thoại ở đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-11.mp3", Japanese = "あなたの　とけい　どこの　とけい　ですか。", Romaji = "Anata no tokei ha doko no tokei desuka.", Vietnamese = "Đồng hồ của bạn là đồng hồ xuất xứ ở đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-12.mp3", Japanese = "あなたの　テイプレコーダー　は　日本の　ですか。", Romaji = "Anata no teipu rekoudaa ha nihon no desuka.", Vietnamese = "Băng ghi âm của bạn là của Nhật phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "3", UrlAudio = @"/Content/media/QA/3-13.mp3", Japanese = "あなたの　とけい　は　いくら　ですか。", Romaji = "Anata no tokei ha ikura desuka.", Vietnamese = "Đồng hồ của bạn bao nhiêu tiền?" },
                
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-1.mp3", Japanese = "今　何時　ですか。", Romaji = "Ima nanji desuka.", Vietnamese = "Bây giờ là mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-2.mp3", Japanese = "ニューヨーク　は　今　何時　ですか。", Romaji = "Nyuuyooku ha ima nanji desuka.", Vietnamese = "NewYork bây giờ mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-3.mp3", Japanese = "銀行　は　何時　から　何時　まで　ですか。", Romaji = "Ginkou ha nanji kara nanji made desuka.", Vietnamese = "Ngân hàng làm từ mấy giờ đến mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-4.mp3", Japanese = "休み　は　何曜日　ですか。", Romaji = "Yasumi ha nan youbi desuka.", Vietnamese = "Nghĩ ngày thứ mấy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-5.mp3", Japanese = "毎晩　何時　に　ねますか。", Romaji = "Maiban nanji ni nemasuka.", Vietnamese = "Mỗi tối ngủ lúc mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-6.mp3", Japanese = "土曜日　はたらきますか。", Romaji = "Doyoubi hatarakimasuka.", Vietnamese = "Thứ bảy có làm việc không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-7.mp3", Japanese = "きのう　べんきょしましたか。", Romaji = "Kinou benkyoshimashitaka.", Vietnamese = "Hôm qua có học bài không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-8.mp3", Japanese = "ＩＭＣのでんわ　ばんごう　は　何ばん　ですか。", Romaji = "IMC no denwa bangou ha nan ban desuka.", Vietnamese = "Số điện thoại của IMC là số mấy?" },

                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-9.mp3", Japanese = "すみません。そちら　は　何時　から　何時　まで　ですか。", Romaji = "Sumimasen. sochira ha nanji kara nanji made desuka", Vietnamese = "Xin phép, bên đó làm từ mấy giờ đến mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-10.mp3", Japanese = "何時に　おわりますか。", Romaji = "Nanji ni owarimasuka.", Vietnamese = "Mấy giờ về?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-11.mp3", Japanese = "毎日　何時に　おきますか。", Romaji = "Mainichi nanji ni okimasuka.", Vietnamese = "Mỗi ngày dậy lúc mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-12.mp3", Japanese = "今朝　何時に　おきましたか。", Romaji = "Kesa nanji ni okimashitaka.", Vietnamese = "Sáng này ngủ dậy lúc mấy giờ?" },
                new MS_ConversationDetailsModels(){ LessonCode = "4", UrlAudio = @"/Content/media/QA/4-13.mp3", Japanese = "昨日　何時　まで　はたらきましたか。", Romaji = "Kinou nanji made hatarakimashitaka.", Vietnamese = "Hôm qua làm việc đến mấy giờ?" },
                
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-1.mp3", Japanese = "明日　どこ　へ　行きますか。", Romaji = "Ashita doko he ikimasuka.", Vietnamese = "Ngày mai đi đến đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-2.mp3", Japanese = "日曜日　どこ　へ　行きましたか。", Romaji = "Nichiyoubi doko he ikimashitaka.", Vietnamese = "Chủ nhật đã đi đến đâu?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-3.mp3", Japanese = "何で　とうきょう　へ　行きますか。", Romaji = "Nande Tokyo he ikimasuka.", Vietnamese = "Đi đến Tokyo bằng gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-4.mp3", Japanese = "だれ　と　とうきょう　へ　行きますか。", Romaji = "Dare to Tokyo he ikimasuka.", Vietnamese = "Đi đến Tokyo với ai vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-5.mp3", Japanese = "いつ　日本　へ　行きましたか。", Romaji = "Itsu Nihon he ikimashitaka.", Vietnamese = "Đã đến Nhật khi nào vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-6.mp3", Japanese = "誕生日　は　いつ　ですか。", Romaji = "Tanjoubi ha itsu desuka.", Vietnamese = "Sinh nhật khi nào vậy?" },

                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-7.mp3", Japanese = "このでんしゃ　は　ＫＯSHIＥＮへ　行きますか。", Romaji = "Kono densha ha Koshien he ikimasuka.", Vietnamese = "Tàu điện này đi đến Koshien phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-8.mp3", Japanese = "だれと　スーパーへ　行きますか。", Romaji = "Dare to suupaa he ikimasuka.", Vietnamese = "Đi siêu thị với ai vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-9.mp3", Japanese = "何で　スーパーへ　行きますか。", Romaji = "Nande suupaa he ikimasuka.", Vietnamese = "Đi đến siêu thị bằng gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-10.mp3", Japanese = "昨日　どこへ　行きましたか。", Romaji = "Kinou doko he ikimashitaka.", Vietnamese = "Hôm qua đã đi đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-11.mp3", Japanese = "誕生日　は　何月　何日　ですか。", Romaji = "Tanjoubi ha nangatsu nannichi desuka.", Vietnamese = "Sinh nhật ngày mấy tháng mấy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "5", UrlAudio = @"/Content/media/QA/5-12.mp3", Japanese = "今日　は　何日　ですか。", Romaji = "Kyou ha nannichi desuka.", Vietnamese = "Hôm nay là ngày mấy?" },

                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-1.mp3", Japanese = "タバコ　を　水ますか。", Romaji = "Tabako wo suimasuka.", Vietnamese = "Hút thuốc à?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-2.mp3", Japanese = "毎朝　何　を　食べますか。", Romaji = "Maiasa nani wo tabemasuka.", Vietnamese = "Mỗi sáng ăn gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-3.mp3", Japanese = "今朝　何　を　食べましたか。", Romaji = "Kesa nani wo tabemashitaka.", Vietnamese = "Sáng này ăn gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-4.mp3", Japanese = "土曜日　何　を　しましたか。", Romaji = "Doyoubi nani wo shimashitaka.", Vietnamese = "Thứ 7 đã làm gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-5.mp3", Japanese = "日曜日 は　何　を　しましたか。", Romaji = "Nichiyoubi ha nani wo shimashitaka.", Vietnamese = "Chủ nhật đã làm gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-6.mp3", Japanese = "どこで　そのかばん　を　貝ましたか。", Romaji = "Doko de sono kaban wo kaimashitaka.", Vietnamese = "Cái cặp này đã mua ở đâu vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-7.mp3", Japanese = "いっしょに　ビール　を　飲みませんか。", Romaji = "Isshoni biiru wo nomimasenka.", Vietnamese = "Cùng đi uống bia không?" },

                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-8.mp3", Japanese = "いつも　このレストラン　で　昼ごはん　を　たべますか。", Romaji = "Itsumo kono resutoran de hirugohan wo tabemasuka.", Vietnamese = "Có thường đến nhà hàng này để ăn trưa không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-9.mp3", Japanese = "今晩　いっしょに　ビール　を　のみませんか。", Romaji = "Konban isshoni biiru wo nomimasenka.", Vietnamese = "Tối nay cùng đi uống bia không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "6", UrlAudio = @"/Content/media/QA/6-10.mp3", Japanese = "いつも　このとしょかん　で　しゅくだい　を　しますか。", Romaji = "Itsumo kono toshokan de shukudai wo shimasuka.", Vietnamese = "Có thường đến thư viện này để làm bài tập không?" },
                
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-1.mp3", Japanese = "テレビ　で　日本語　を　べんきょしましたか。", Romaji = "Terebi de Nihongo wo benkyoshimashitaka.", Vietnamese = "Đã học tiếng Nhật bằng tivi phài không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-2.mp3", Japanese = "日本語　で　レポート　を　書きますか", Romaji = "Nihongo de repooto wo kakimasuka.", Vietnamese = "Viết báo cáo bằng tiếng Nhật phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-3.mp3", Japanese = "ＧＯＯＤＢＹＥ　は　日本語で　何　ですか。", Romaji = "Goodbye ha Nihongo de nan desuka.", Vietnamese = "Goodbye tiếng Nhật là gì?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-4.mp3", Japanese = "だれに　クリスマスカード　を　書きますか。", Romaji = "Dare ni kurisumasu kaado wo kakimasuka.", Vietnamese = "Viết thiệp giáng sinh cho ai vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-5.mp3", Japanese = "それ　は　何　ですか。", Romaji = "Sore ha nan desuka.", Vietnamese = "Đó là cái gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-6.mp3", Japanese = "もう　しんかんせんの　きっぷ　を　買いましたか", Romaji = "Mou Shinkansen no kippu wo kaimashitaka.", Vietnamese = "Đã mua vé tàu cao tốc chưa?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-7.mp3", Japanese = "もう　昼ごはん　を　食べましたか。", Romaji = "Mou hirugohan wo tabemashitaka.", Vietnamese = "Đã ăn trưa chưa?" },

                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-8.mp3", Japanese = "そのしゃつ　すてき　ですね。", Romaji = "Sono shatsu suteki desune.", Vietnamese = "Áo đó đẹp nhỉ" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-9.mp3", Japanese = "そのとけい　すてき　ですね。", Romaji = "Sono tokei suteki desune.", Vietnamese = "Đồng hồ đó đẹp nhỉ" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-10.mp3", Japanese = "何で　ごはん　を　たべますか。", Romaji = "Nande gohan wo tabemasuka.", Vietnamese = "Ăn bằng gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-11.mp3", Japanese = "去年の　誕生日に　プレゼント　を　もらいましたか。", Romaji = "Kyonen no tanjoubi ni purezento wo moraimashitaka.", Vietnamese = "Sinh nhật năm rồi có được tăng quà phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-12.mp3", Japanese = "お母さんの　誕生日に　何　を　あげましたか。", Romaji = "Okaasan no tanjoubi ni nani wo agemasuka.", Vietnamese = "Sinh nhật của mẹ bạn tặng gì vậy?" },
                new MS_ConversationDetailsModels(){ LessonCode = "7", UrlAudio = @"/Content/media/QA/7-13.mp3", Japanese = "ＴＨＡＮＫ　ＹＯＵ　は　日本語　で　何　ですか。", Romaji = "Thank you ha Nihongo de nan desuka.", Vietnamese = "Thank you tiếng Nhật là gì?" },

                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-3.mp3", Japanese = "ペキン　は　今　さむい　ですか。", Romaji = "Pekin ha ima samui desuka.", Vietnamese = "Bắc Kinh bây giờ lạnh phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-4.mp3", Japanese = "そのじしょ　は　いい　ですか。", Romaji = "Sono jisho ha ii desuka.", Vietnamese = "Từ điển đó tốt phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-5.mp3", Japanese = "とおきょのちかてつ　は　どう　ですか。", Romaji = "Tookyo no chikatetsu ha dou desuka.", Vietnamese = "Tàu điện ngày của Tokyo thế nào?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-6.mp3", Japanese = "どんな　えいが　ですか。", Romaji = "Donna eiga desuka.", Vietnamese = "Bộ phim thế nào?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-8.mp3", Japanese = "おげんき　ですか。", Romaji = "Ogenki desuka.", Vietnamese = "Bạn khỏe không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-9.mp3", Japanese = "おしごと　は　どう　ですか。", Romaji = "Oshigoto ha dou desuka.", Vietnamese = "Công việc của bạn thế nào?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-10.mp3", Japanese = "日本語のべんきょう　は　どう　ですか。", Romaji = "Nihongo no benkyou ha dou desuka.", Vietnamese = "Việc học tiếng Nhật thế nào?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-11.mp3", Japanese = "大学のりょう　は　どう　ですか。", Romaji = "Daigaku no ryou ha dou desuka.", Vietnamese = "KTX của trường đại học thế nào?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-12.mp3", Japanese = "かぞく　は　げんき　ですか。", Romaji = "Kazoku ha genki desuka.", Vietnamese = "Gia đình khỏe phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-13.mp3", Japanese = "あなたの国　は　いま　あつい　ですか。", Romaji = "Anata no kuni ha ima atsui desuka.", Vietnamese = "Đất nước của bạn bây giờ nóng phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-14.mp3", Japanese = "しごと　は　おもしろい　ですか。", Romaji = "Shigoto ha omoshiroi desuka.", Vietnamese = "Công việc thú vị phải không?" },
                new MS_ConversationDetailsModels(){ LessonCode = "8", UrlAudio = @"/Content/media/QA/8-15.mp3", Japanese = "あなたの国　は　どんな国　ですか。", Romaji = "Anata no kuni ha donna kuni desuka.", Vietnamese = "Đất nước của bạn là đất nước thế nào?" },
            };

            //MS_ConversationDetailsModels condition = new MS_ConversationDetailsModels();
            //condition.LessonCode = lessonCode;
            //int returnCode = (new MS_ConversationsDao()).SelectConversationTestData(condition, out details);
            if (!CommonMethod.IsNullOrEmpty(lessonCode))
            {
                string[] ids = lessonCode.Split(',');
                details = details.Where(ss => ids.Contains(ss.LessonCode)).ToList();
            }

            return Json(new
            {
                conversationDetails = RandomList(details, details.Count)
                //conversationDetails = (details)
            }, JsonRequestBehavior.AllowGet);
        }

        private List<MS_ConversationDetailsModels> RandomList(List<MS_ConversationDetailsModels> list, int length)
        {
            List<MS_ConversationDetailsModels> results = new List<MS_ConversationDetailsModels>();

            int numOfWords = list.Count;
            while (list.Count > 0)
            {
                //get random position
                Random random = new Random();
                int number = random.Next(list.Count);

                //add the selected item to result list
                results.Add(list[number]);

                //remove the selected item
                list.RemoveAt(number);
            }
            return results;
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetVocabularies(string lessonCode)
        {
            int returnCode = 0;
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
            //{
            //    LessonCode = lessonCode,
            //    UserName = UserSession.UserName,
            //};
            //if (CommonMethod.IsNullOrEmpty(UserSession.UserName))
            //{
            //    returnCode = vocaDao.SelectDataForLesson(model, out results);
            //}
            //else
            //{
            //    returnCode = vocaDao.SelectVocaForLesson(model, out results);
            //}

            return Json(new { vocabularies = RandomList(results, results.Count) }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRandomVoca()
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            model.UserName = UserSession.UserName;
            int returnCode = vocaDao.SelectWeakVocaForLesson(model, out results);
            var voca = RandomList(results, results.Count).FirstOrDefault();
            return Json(new { Voca = voca }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetWeakVocabularies(string lessonCode)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            //model.LessonCode = lessonCode;
            //model.UserName = UserSession.UserName;
            //int returnCode = vocaDao.SelectWeakVocaForLesson(model, out results);

            return Json(new { vocabularies = RandomList(results, results.Count) }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetHasLearntVocabularies(string lessonCode)
        {
            int returnCode = 0;
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
            //{
            //    LessonCode = lessonCode,
            //    UserName = UserSession.UserName,
            //    HasLearnt = CommonData.Status.Enable,
            //};
            //if (!CommonMethod.IsNullOrEmpty(UserSession.UserName))
            //{
            //    returnCode = vocaDao.SelectVocaForLesson(model, out results);
            //}

            return Json(new { vocabularies = RandomList(results, results.Count) }, JsonRequestBehavior.AllowGet);
        }


        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetTests(string lessonCode)
        {
            int returnCode = 0;
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
            //{
            //    LessonCode = lessonCode,
            //    UserName = UserSession.UserName,
            //};
            //if (CommonMethod.IsNullOrEmpty(UserSession.UserName))
            //{
            //    returnCode = vocaDao.SelectDataForLesson(model, out results);
            //}
            //else
            //{
            //    returnCode = vocaDao.SelectVocaForLesson(model, out results);
            //}

            return Json(new { vocabularies = RandomList(CreateTestList(results), results.Count) }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetWeakTests(string lessonCode)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels();
            //model.LessonCode = lessonCode;
            //model.UserName = UserSession.UserName;
            //int returnCode = vocaDao.SelectWeakVocaForLesson(model, out results);

            return Json(new { vocabularies = RandomList(CreateWeakTestList(results), results.Count) }, JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetHasLearntTests(string lessonCode)
        {
            int returnCode = 0;
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();
            //MS_UserVocabulariesModels model = new MS_UserVocabulariesModels()
            //{
            //    LessonCode = lessonCode,
            //    UserName = UserSession.UserName,
            //    HasLearnt = CommonData.Status.Enable,
            //};
            //if (!CommonMethod.IsNullOrEmpty(UserSession.UserName))
            //{
            //    returnCode = vocaDao.SelectVocaForLesson(model, out results);
            //}

            return Json(new { vocabularies = RandomList(CreateTestList(results), results.Count) }, JsonRequestBehavior.AllowGet);
        }

        [EncryptActionName(Name = ("GetWords"))]
        [OutputCache(CacheProfile = "Cache5MinutesVaryByIDClient")]
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult GetWords(bool hasNormal, bool hasDA, bool hasLongSound, bool hasTsu, bool hasAA)
        {
            MS_VocabulariesDao vocaDao = new MS_VocabulariesDao();
            List<MS_VocabulariesModels> results = new List<MS_VocabulariesModels>();
            MS_VocabulariesModels conditionModel = new MS_VocabulariesModels()
            {
                //1: alphabet
                //2: word
                Type = "1",
                HasCombination = hasAA,
                HasDiacritic = hasDA,
                HasTsu = hasTsu,
                HasNormal = hasNormal,
                HasLongSound = hasLongSound,
            };
            int returnCode = vocaDao.SelectData(conditionModel, out results);

            return Json(new { words = RandomWordList(results, results.Count) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateVocas(List<MS_UserVocabulariesModels> vocas)
        {
            int returnCode = 0;
            if (!CommonMethod.IsNullOrEmpty(UserSession.UserName))
            {
                MS_VocabulariesDao dao = new MS_VocabulariesDao();
                returnCode = dao.UpdateLevelData(vocas);
            }

            return Json(new { ReturnCode = returnCode });
        }

        private List<MS_UserVocabulariesModels> CreateTestList(List<MS_UserVocabulariesModels> list)
        {
            if (list.Count < 4)
            {
                return list;
            }
            List<MS_UserVocabulariesModels> results = list;
            Random random = new Random();
            Random r2 = new Random();
            Random r3 = new Random();
            Random r4 = new Random();
            int number = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            foreach (var item in results)
            {
                //tìm vị trí đặt kết quả đúng (1 -> 4)
                number = random.Next(0, 5);
                switch (number)
                {
                    case 1:
                        item.CorrectResult = 1;
                        item.Result1 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 2:
                        item.CorrectResult = 2;
                        item.Result2 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio2 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 3:
                        item.CorrectResult = 3;
                        item.Result3 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio3 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 4:
                        item.CorrectResult = 4;
                        item.Result4 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio4 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result3 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio3 = results[n4].UrlAudio;
                        break;
                    default:
                        item.CorrectResult = 1;
                        item.Result1 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                }
            }

            return results;
        }

        private List<MS_UserVocabulariesModels> CreateWeakTestList(List<MS_UserVocabulariesModels> list)
        {
            if (list.Count < 4)
            {
                return list;
            }
            List<MS_UserVocabulariesModels> results = list;
            
            Random random = new Random();
            Random r2 = new Random();
            Random r3 = new Random();
            Random r4 = new Random();
            int number = 0;
            int n2 = 0;
            int n3 = 0;
            int n4 = 0;
            foreach (var item in results)
            {
                //tìm vị trí đặt kết quả đúng (1 -> 4)
                number = random.Next(0, 5);
                switch (number)
                {
                    case 1:
                        item.CorrectResult = 1;
                        item.Result1 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 2:
                        item.CorrectResult = 2;
                        item.Result2 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio2 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 3:
                        item.CorrectResult = 3;
                        item.Result3 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio3 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                    case 4:
                        item.CorrectResult = 4;
                        item.Result4 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio4 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result1 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio1 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result2 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio2 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result3 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio3 = results[n4].UrlAudio;
                        break;
                    default:
                        item.CorrectResult = 1;
                        item.Result1 = item.Hiragana + "; " + item.Katakana;
                        item.ResultUrlAudio1 = item.UrlAudio;

                        while (item.VocabularyCode == results[n2].VocabularyCode)
                        {
                            n2 = r2.Next(list.Count);
                        }
                        item.Result2 = results[n2].Hiragana + "; " + results[n2].Katakana;
                        item.ResultUrlAudio2 = results[n2].UrlAudio;

                        while (item.VocabularyCode == results[n3].VocabularyCode || results[n2].VocabularyCode == results[n3].VocabularyCode)
                        {
                            n3 = r3.Next(list.Count);
                        }
                        item.Result3 = results[n3].Hiragana + "; " + results[n3].Katakana;
                        item.ResultUrlAudio3 = results[n3].UrlAudio;

                        while (item.VocabularyCode == results[n4].VocabularyCode || results[n2].VocabularyCode == results[n4].VocabularyCode || results[n3].VocabularyCode == results[n4].VocabularyCode)
                        {
                            n4 = r4.Next(list.Count);
                        }
                        item.Result4 = results[n4].Hiragana + "; " + results[n4].Katakana;
                        item.ResultUrlAudio4 = results[n4].UrlAudio;
                        break;
                }
            }

            return results;
        }


        private List<MS_UserVocabulariesModels> RandomList(List<MS_UserVocabulariesModels> list, int length)
        {
            List<MS_UserVocabulariesModels> results = new List<MS_UserVocabulariesModels>();

            int numOfWords = list.Count;
            while (list.Count > 0)
            {
                //get random position
                Random random = new Random();
                int number = random.Next(list.Count);

                //add the selected item to result list
                results.Add(list[number]);

                //remove the selected item
                list.RemoveAt(number);
            }
            return results;
        }

        private List<MS_VocabulariesModels> RandomWordList(List<MS_VocabulariesModels> list, int length)
        {
            List<MS_VocabulariesModels> results = new List<MS_VocabulariesModels>();

            int numOfWords = list.Count;
            while (list.Count > 0)
            {
                //get random position
                Random random = new Random();
                int number = random.Next(list.Count);

                //add the selected item to result list
                results.Add(list[number]);

                //remove the selected item
                list.RemoveAt(number);
            }
            return results;
        }
    }

}
