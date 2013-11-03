using UnityEngine;
using System.Collections;

public interface Wave {
	float GetHeightInfluence(float x, float y);

	// returns true if alive
	bool Update(float deltaTime);
}
