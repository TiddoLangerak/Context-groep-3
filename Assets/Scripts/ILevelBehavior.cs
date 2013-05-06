public interface ILevelBehavior
{
    /// <summary>
    /// Makes a level block.
    /// </summary>
    /// <returns>
    /// The level block.
    /// </returns>
    /// <param name='position'>
    /// The position on the z axis of the levelBlock.
    /// </param>
    object makeLevelBlock(float pos);
    /// <summary>
    /// Destroys a level block.
    /// </summary>
    /// <param name='levelBlock'>
    /// The level block to be destroyed.
    /// </param>
    void destroyObject(object gameObject);

    object makeObstacle(int line, float position);
	object makePowerUp(int line, float position);
}