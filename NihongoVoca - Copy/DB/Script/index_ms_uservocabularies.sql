USE [nihongo_voca]
GO

/****** Object:  Index [IX_ms_uservocabularies]    Script Date: 07/25/2015 23:05:42 ******/
CREATE NONCLUSTERED INDEX [IX_ms_uservocabularies] ON [dbo].[ms_uservocabularies] 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO


