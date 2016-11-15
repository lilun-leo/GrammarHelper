using GrammarHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace text.Extenions
{
    public partial class Json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //  List<user> list = new List<user>();
            //  user u = new user() { id="1",name="22"};
            //var aaaa=  JsonHelper.ToJson(u);
            //  list.Add(u);

            //var a1 = JsonHelper.ListToJson(list,"");

            // string[] a = { "aa","bb","cc"};
            //var aa= JsonHelper.ArrayToJson(a);
            string json = "{'code':0,'msg':null,'clientId':null,'notes':null,'body':{'pageNumber':1,'pageSize':999999,'total':3,'bean':null,'rows':[{'id':'0000000057fa611001582823660e2938','name':'4901347811.wav','tags':null,'fileSize':255020,'fileNum':null,'permission':'changepermission','ownerId':'team_20161017132748882_9b53d997d61543a38a5ff1964ea75c1f','creatorName':'张秀翠','creatorId':'5e28ccce - 8729 - 4377 - b7ff - e3ebd3c21493','type':'file','parentId':'0000000057a918820157d11c33933c12','createTime':1478142158000,'lastUpdateTime':1478142158000,'createIP':'','lastUpdateIP':'','versionId':'0000000057fa611001582823660e2939','path':' / 4901347811.wav','sharerName':null,'deadline':null,'status':'normal','totaldown':3,'totalshare':0,'currentshare':0,'canDown':true,'lockstatus':0,'lockuser':null,'deleteUser':null,'deleteUserId':null,'deleteUserImgType':null,'deleteUserImgFileId':null,'backupTeamPath':null,'backupParentId':null,'backupTeamId':null,'currentdown':0,'currentpreviews':0,'hash':'12031d859a12b549f17f2498a21c20b4','versionNum':'00000','fileType':'wav','fileBeanType':null,'sharePermission':null,'shareEventId':null,'uploader':null,'commentCount':0,'commentEnable':true,'imgType':'default','imgFileId':'100','isKillVirus':0},{'id':'0000000057fa61100158282361602934','name':'4901347811 - 2.wav','tags':null,'fileSize':147756,'fileNum':null,'permission':'changepermission','ownerId':'team_20161017132748882_9b53d997d61543a38a5ff1964ea75c1f','creatorName':'张秀翠','creatorId':'5e28ccce - 8729 - 4377 - b7ff - e3ebd3c21493','type':'file','parentId':'0000000057a918820157d11c33933c12','createTime':1478142157000,'lastUpdateTime':1478142157000,'createIP':'','lastUpdateIP':'','versionId':'0000000057fa61100158282361612935','path':' / 4901347811 - 2.wav','sharerName':null,'deadline':null,'status':'normal','totaldown':0,'totalshare':0,'currentshare':0,'canDown':true,'lockstatus':0,'lockuser':null,'deleteUser':null,'deleteUserId':null,'deleteUserImgType':null,'deleteUserImgFileId':null,'backupTeamPath':null,'backupParentId':null,'backupTeamId':null,'currentdown':0,'currentpreviews':0,'hash':'acfe99f3279022d962ac58a205b0fdce','versionNum':'00000','fileType':'wav','fileBeanType':null,'sharePermission':null,'shareEventId':null,'uploader':null,'commentCount':0,'commentEnable':true,'imgType':'default','imgFileId':'100','isKillVirus':0},{'id':'0000000057fa6110015828235f0a2930','name':'4901347811 - 1.wav','tags':null,'fileSize':258348,'fileNum':null,'permission':'changepermission','ownerId':'team_20161017132748882_9b53d997d61543a38a5ff1964ea75c1f','creatorName':'张秀翠','creatorId':'5e28ccce - 8729 - 4377 - b7ff - e3ebd3c21493','type':'file','parentId':'0000000057a918820157d11c33933c12','createTime':1478142156000,'lastUpdateTime':1478142156000,'createIP':'','lastUpdateIP':'','versionId':'0000000057fa6110015828235f0a2931','path':' / 4901347811 - 1.wav','sharerName':null,'deadline':null,'status':'normal','totaldown':0,'totalshare':0,'currentshare':0,'canDown':true,'lockstatus':0,'lockuser':null,'deleteUser':null,'deleteUserId':null,'deleteUserImgType':null,'deleteUserImgFileId':null,'backupTeamPath':null,'backupParentId':null,'backupTeamId':null,'currentdown':0,'currentpreviews':0,'hash':'46ff7db336ad10f073fdde0de62100b7','versionNum':'00000','fileType':'wav','fileBeanType':null,'sharePermission':null,'shareEventId':null,'uploader':null,'commentCount':0,'commentEnable':true,'imgType':'default','imgFileId':'100','isKillVirus':0}],'custom':{}}}";
            var jsoncalss= ConvertJson.JsonConvertObjice(json);





        }
        public class user {

           public string id { get; set; }
            public string name { get; set; }
        }
    
    }


}