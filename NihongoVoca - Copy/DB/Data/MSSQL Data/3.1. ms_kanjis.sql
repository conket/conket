use nihongo_voca
go

DELETE FROM ms_uservocabularies;
DBCC CHECKIDENT ('ms_uservocabularies', RESEED, 1);
DELETE FROM ms_vocabularydetails;
DBCC CHECKIDENT ('ms_vocabularydetails', RESEED, 1);
DELETE FROM ms_vocakanjis;
DBCC CHECKIDENT ('ms_vocakanjis', RESEED, 1);
DELETE FROM ms_kanjiexamples;
DBCC CHECKIDENT ('ms_kanjiexamples', RESEED, 1);
DELETE FROM ms_answers;
DBCC CHECKIDENT ('ms_answers', RESEED, 1);
DELETE FROM ms_kanjis;
DBCC CHECKIDENT ('ms_kanjis', RESEED, 1);

INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010001',N'3',N'一',N'Nhất',N'',N'MỘT ngón tay',N'0',N'イチ。イッ。イツ',N'ひと',N'Một',N'Nhất','/Images/Voca/V000010001.jpg','/Content/media/kanji/V000010001.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010002',N'3',N'二',N'Nhị',N'',N'HAI ngón tay',N'0',N'ニ',N'ふた',N'Hai',N'Nhị','/Images/Voca/V000010002.jpg','/Content/media/kanji/V000010002.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010003',N'3',N'三',N'Tam',N'',N'BA ngón tay',N'0',N'サン',N'み。みっ',N'Ba',N'Tam','/Images/Voca/V000010003.jpg','/Content/media/kanji/V000010003.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010004',N'3',N'四',N'Tứ',N'',N'Hình chữ nhật có BỐN góc vuông',N'0',N'シ',N'よ。よん。よっ',N'Bốn',N'Tứ','/Images/Voca/V000010004.jpg','/Content/media/kanji/V000010004.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010005',N'3',N'五',N'Ngũ',N'',N'Viết chữ Ngũ với NĂM cây que',N'0',N'ゴ',N'いつ',N'Năm',N'Ngũ','/Images/Voca/V000010005.jpg','/Content/media/kanji/V000010005.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010006',N'3',N'六',N'Lục',N'',N'Nắm bàn tay xòe 2 ngón cái và ngón út là dấu hiệu số SÁU',N'0',N'ロク。ロッ',N'む。むつ。むい',N'Sáu',N'Lục','/Images/Voca/V000010006.jpg','/Content/media/kanji/V000010006.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010007',N'3',N'七',N'Thất',N'',N'Số BẢY viết ngược',N'0',N'シチ',N'なな。なの',N'Bảy',N'Thất','/Images/Voca/V000010007.jpg','/Content/media/kanji/V000010007.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010008',N'3',N'八',N'Bát',N'',N'Giống chữ HA (ハ) trong Katakana',N'0',N'ハチ。ハッ',N'や。よう。やっ',N'Tám',N'Bát','/Images/Voca/V000010008.jpg','/Content/media/kanji/V000010008.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010009',N'3',N'九',N'Cửu',N'',N'Một người đang chống đẩy CHÍN cái',N'0',N'キュウ。ク',N'ここの',N'Chín',N'Cửu','/Images/Voca/V000010009.jpg','/Content/media/kanji/V000010009.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010010',N'3',N'十',N'Thập',N'',N'Kí hiệu chữ THẬP đỏ',N'0',N'ジュウ。ジッ',N'と。とう',N'Mười',N'Thập','/Images/Voca/V000010010.jpg','/Content/media/kanji/V000010010.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010011',N'3',N'百',N'Bách',N'',N'Số 100 nằm ngang 1 góc 90 độ',N'0',N'ヒャク。ビャク。ピャク',N'',N'Trăm',N'Bách','/Images/Voca/V000010011.jpg','/Content/media/kanji/V000010011.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010012',N'3',N'千',N'Thiên',N'',N'Chữ Thập thêm dấu phẩy trên đầu',N'0',N'セン。ゼン',N'ち',N'Ngàn',N'Thiên','/Images/Voca/V000010012.jpg','/Content/media/kanji/V000010012.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010013',N'3',N'万',N'Vạn',N'',N'1 vạn (10000) có 5 chữ số 0 nên viết giống số 5',N'0',N'マン。バン',N'',N'Chục ngàn',N'Vạn','/Images/Voca/V000010013.jpg','/Content/media/kanji/V000010013.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010014',N'3',N'円',N'Viên',N'',N'Hình dạng VÒNG TRÒN',N'0',N'エン',N'まる',N'Hình tròn / Đồng yen Nhật',N'Viên','/Images/Voca/V000010014.jpg','/Content/media/kanji/V000010014.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010015',N'3',N'口',N'Khẩu',N'',N'HÌnh dạng cái MIỆNG đang há to',N'0',N'コウ。ク',N'くち。ぐち',N'Cái miệng',N'Khẩu','/Images/Voca/V000010015.jpg','/Content/media/kanji/V000010015.mp3');
INSERT INTO ms_kanjis(Code, DisplayType, Kanji, Pinyin, Writing, Remembering, Strokes, OnReading, KunReading, VMeaning, Description, UrlImage, UrlAudio) VALUES('V000010016',N'3',N'目',N'Mục',N'',N'Hinh dạng con MẮT quay dọc 90 độ',N'0',N'モク。ボク',N'め。ま',N'Con mắt',N'Mục','/Images/Voca/V000010016.jpg','/Content/media/kanji/V000010016.mp3');
