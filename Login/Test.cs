//using System;
//using System.Collections;
//using System.Configuration;
//using MySql.Data;
//using MySql.Data.MySqlClient;
//using System.Data;

////按登录按钮，核对输入的数据
//private void button1_Click(object sender, EventArgs e)
//{
//    //后面拼写查询语句要用到窗体的信息
//    string user = textBox1.Text;
//    string pwd = textBox2.Text;
//    //创建数据库连接类的对象
//    SqlConnection con = new SqlConnection("server=.;database=data1220;user=sa;pwd=123");
//    //将连接打开
//    con.Open();
//    //执行con对象的函数，返回一个SqlCommand类型的对象
//    SqlCommand cmd = con.CreateCommand();
//    //把输入的数据拼接成sql语句，并交给cmd对象
//    cmd.CommandText = "select*from users where name='" + user + "'and pwd='" + pwd + "'";

//    //用cmd的函数执行语句，返回SqlDataReader对象dr,dr就是返回的结果集（也就是数据库中查询到的表数据）
//    SqlDataReader dr = cmd.ExecuteReader();
//    //用dr的read函数，每执行一次，返回一个包含下一行数据的集合dr，在执行read函数之前，dr并不是集合
//    if (dr.Read())
//    {
//        //dr[]里面可以填列名或者索引，显示获得的数据
//        MessageBox.Show(dr[1].ToString());
//    }
//    //用完后关闭连接，以免影响其他程序访问

//private void button2_Click(object sender, EventArgs e)
//{
//    GetMessage();
//}

//private DataTable GetMessage()
//{
//    string P_Str_ConnectionStr = string.Format("server={0};user id = {1};port = {2};database=my_db2;pooling = false;", "localhost", "root", 3306);
//    string P_Str_SqlStr = string.Format("SELECT id,name FROM student");
//    MySqlDataAdapter adapter = new MySqlDataAdapter(P_Str_SqlStr, P_Str_ConnectionStr);
//    DataTable P_dt = new DataTable();
//    adapter.Fill(P_dt);
//    this.dataGridView1.DataSource = P_dt;
//    return P_dt;
//}
//
//using MySql.Data.MySqlClient;

//String connetStr = "server=127.0.0.1;port=3306;user=root;password=root; database=minecraftdb;";
//// server=127.0.0.1/localhost 代表本机，端口号port默认是3306可以不写
//MySqlConnection conn = new MySqlConnection(connetStr);
//try
//{    
//      conn.Open();//打开通道，建立连接，可能出现异常,使用try catch语句
//      Console.WriteLine("已经建立连接");
//      //在这里使用代码对数据库进行增删查改
//}
//catch (MySqlException ex)
//{
//      Console.WriteLine(ex.Message);
//}
//finally
//{
//      conn.Close();
//}


//    con.Close();
//}