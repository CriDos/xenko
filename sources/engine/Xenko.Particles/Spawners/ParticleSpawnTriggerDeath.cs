// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using Xenko.Core;
using Xenko.Core.Mathematics;

namespace Xenko.Particles.Spawners
{
    /// <summary>
    /// <see cref="ParticleSpawnTriggerDeath"/> triggers when the parent particle dies
    /// </summary>
    [DataContract("ParticleSpawnTriggerDeath")]
    [Display("On Death")]
    public class ParticleSpawnTriggerDeath : ParticleSpawnTrigger<float>
    {
        public override void PrepareFromPool(ParticlePool pool)
        {
            if (pool == null)
            {
                FieldAccessor = ParticleFieldAccessor<float>.Invalid();
                return;
            }

            FieldAccessor = pool.GetField(ParticleFields.RemainingLife);
        }

        public unsafe override float HasTriggered(Particle parentParticle)
        {
            if (!FieldAccessor.IsValid())
                return 0f;

            var remainingLifetime = (*((float*)parentParticle[FieldAccessor]));

            return (remainingLifetime <= MathUtil.ZeroTolerance) ? 1f : 0f;
        }
    }
}
