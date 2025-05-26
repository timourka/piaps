using System;

namespace repositories {
	public interface Repository {
		void Create(ref Models.Model object_);
		Models.Model[] Read();
		void Update(ref Models.Model objectBefore, ref Models.Model objectAfter);
		void Delete(ref Models.Model object_);

	}

}
