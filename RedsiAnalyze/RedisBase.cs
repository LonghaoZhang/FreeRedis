using FreeRedis;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Diagnostics;

namespace RedsiAnalyze
{
    public class RedisBase
	{
		
		static Lazy<RedisClient> _cliLazyTest = new Lazy<RedisClient>(() =>
		{

			var constr = ConfigurationManager.AppSettings["redis.config.test"];
			var r = new RedisClient(constr); //redis 6.0
												 // var r = new RedisClient(connectionString); //redis 6.0
			r.Serialize = obj => JsonConvert.SerializeObject(obj);
			r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
			r.Notice += (s, e) => Trace.WriteLine(e.Log);
			return r;
		});
		static Lazy<RedisClient> _cliLazyMaster = new Lazy<RedisClient>(() =>
		{

			var constr = ConfigurationManager.AppSettings["redis.config.master"];
			var r = new RedisClient(constr); //redis 6.0
											 // var r = new RedisClient(connectionString); //redis 6.0
			r.Serialize = obj => JsonConvert.SerializeObject(obj);
			r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
			r.Notice += (s, e) => Trace.WriteLine(e.Log);
			return r;
		});
		static Lazy<RedisClient> _cliLazyLarge = new Lazy<RedisClient>(() =>
		{

			var constr = ConfigurationManager.AppSettings["redis.config.large.master"];
			var r = new RedisClient(constr); //redis 6.0
											 // var r = new RedisClient(connectionString); //redis 6.0
			r.Serialize = obj => JsonConvert.SerializeObject(obj);
			r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
			r.Notice += (s, e) => Trace.WriteLine(e.Log);
			return r;
		});
		//static Lazy<RedisClient> _cliLazyCache0 = new Lazy<RedisClient>(() =>
		//{

		//	var constr = ConfigurationManager.AppSettings["redis.config.cache0"];
		//	var r = new RedisClient(constr); //redis 6.0
		//									 // var r = new RedisClient(connectionString); //redis 6.0
		//	r.Serialize = obj => JsonConvert.SerializeObject(obj);
		//	r.Deserialize = (json, type) => JsonConvert.DeserializeObject(json, type);
		//	r.Notice += (s, e) => Trace.WriteLine(e.Log);
		//	return r;
		//});
		public static RedisClient cliTest => _cliLazyTest.Value;
		//public static RedisClient cliCache0 => _cliLazyCache0.Value;
		public static RedisClient cliMaster => _cliLazyMaster.Value;
		public static RedisClient cliLargeMaster => _cliLazyLarge.Value;

		public static RedisClient cli(int db)
		{
			if (db == 1)
				return cliTest;
			if (db == 2)
				return cliMaster;
			if (db == 3)
				return cliLargeMaster;
			//if (db == 4)
			//	return cliCache0;
			return cliTest;
		}
	}
}
