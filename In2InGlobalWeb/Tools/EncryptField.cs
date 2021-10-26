using System.Data;


namespace InGlobal.presentation
{
    public class EncryptField
    {
        public string Encrypt(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                string DBKey = "s@m@41";
                string AppKey = "12$%";
                string ServerKey = "it-tpm-kss"; //for more security enable -->FingerPrint.Value(); //File.ReadAllText("D:\\kss-ittpm\\file.txt");
                string key = DBKey + AppKey + ServerKey;
                string EncryptedText = DataSecurity.Encrypt(field, "^!#w57@t#", key, "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
                return EncryptedText;
            }
            else
            {
                return string.Empty;
            }




        }
        public string Decrypt(string field)
        {
            if (!string.IsNullOrEmpty(field))
            {
                string DBKey = "s@m@41";
                string AppKey = "12$%";
                string ServerKey = "it-tpm-kss";// FingerPrint.Value();
                string key = DBKey + AppKey + ServerKey;
                string DecryptedText = DataSecurity.Decrypt(field, "^!#w57@t#", key, "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
                return DecryptedText;
            }
            else
            {
                return string.Empty;
            }

        }

        public DataTable DecryptDataColoumns(DataTable dataTable)
        {
            string DBKey = "s@m@41";
            string AppKey = "12$%";
            string ServerKey = "it-tpm-kss";// File.ReadAllText("D:\\SMART-Ra\\file.txt");
            string key = DBKey + AppKey + ServerKey;
            int count = dataTable.Columns.Count;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                for (int j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (dataTable.Columns[j].ColumnName.Contains("en_"))
                    {
                        if (!string.IsNullOrEmpty(dataTable.Rows[i][j].ToString()))
                        {

                            dataTable.Rows[i][j] = DataSecurity.Decrypt(dataTable.Rows[i][j].ToString(), "^!#w57@t#", key, "SHA1", 2, "@1B2c3D4e5F6g7H8", 256);
                        }
                    }

                }
            }

            return dataTable;
        }
    }

}