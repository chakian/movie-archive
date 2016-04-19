using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace com.cagdaskorkut.utility {
	public class BaseDb {
		private SqlConnection connection;
		private SqlCommand command;
		private List<SqlParameter> parameters;

		private void openConnection ( ) {
			if ( connection.State == ConnectionState.Closed )
				connection.Open ( );
		}
		private void closeConnection ( ) {
			if ( connection != null && connection.State != System.Data.ConnectionState.Closed )
				connection.Close ( );
		}
		private void addParameter ( string key, object value, DbType type ) {
			parameters.Add ( new SqlParameter { DbType = type, ParameterName = key, Value = value } );
		}

		public BaseDb ( ) {
		}
		public BaseDb ( string connectionString ) {
			SetConnectionString ( connectionString );
		}

		protected void SetConnectionString ( string connectionString ) {
			connection = new SqlConnection ( connectionString );
		}

		protected void PrepareNewCommand ( string commandText, CommandType commandType ) {
			command = new SqlCommand ( );
			command.Connection = connection;
			command.CommandType = commandType;
			command.CommandText = commandText;
			parameters = new List<SqlParameter> ( );
		}
		protected void PrepareNewCommand ( string commandText ) {
			PrepareNewCommand ( commandText, CommandType.Text );
		}

		protected void AddString ( string key, string value ) {
			addParameter ( key, value, DbType.String );
		}
		protected void AddInteger ( string key, int value ) {
			addParameter ( key, value, DbType.Int32 );
		}
		protected void AddBigInteger ( string key, long value ) {
			addParameter ( key, value, DbType.Int64 );
		}
		protected void AddTinyInteger ( string key, int value ) {
			addParameter ( key, value, DbType.Int16 );
		}
		protected void AddDateTime ( string key, DateTime value ) {
			addParameter ( key, value, DbType.Date );
		}
		protected void AddDouble ( string key, double value ) {
			addParameter ( key, value, DbType.Decimal );
		}

		protected void ExecuteNonQuery ( ) {
			openConnection ( );
			command.Parameters.AddRange ( parameters.ToArray<SqlParameter> ( ) );
			command.ExecuteNonQuery ( );
			closeConnection ( );
		}
		protected DataTable ExecuteReader ( ) {
			openConnection ( );

			command.Parameters.AddRange ( parameters.ToArray<SqlParameter> ( ) );

			DataSet dataset = new DataSet ( );
			SqlDataAdapter adapter = new SqlDataAdapter ( command );
			adapter.Fill ( dataset );

			closeConnection ( );

			if ( dataset != null && dataset.Tables.Count > 0 )
				return dataset.Tables[0];
			else
				return new DataTable ( );
		}
	}
}