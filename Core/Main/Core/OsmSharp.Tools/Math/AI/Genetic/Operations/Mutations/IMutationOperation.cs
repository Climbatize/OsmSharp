﻿// OsmSharp - OpenStreetMap tools & library.
// Copyright (C) 2012 Abelshausen Ben
// 
// This file is part of OsmSharp.
// 
// OsmSharp is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 2 of the License, or
// (at your option) any later version.
// 
// OsmSharp is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with OsmSharp. If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Tools.Math.AI.Genetic.Solvers;

namespace OsmSharp.Tools.Math.AI.Genetic.Operations.Mutations
{
    /// <summary>
    /// Interface abstracting the usage of a specific mutation implementation.
    /// </summary>
    /// <typeparam name="GenomeType"></typeparam>
    public interface IMutationOperation<GenomeType, ProblemType, WeightType>
        where ProblemType : IProblem
        where GenomeType : class
        where WeightType : IComparable
    {
        /// <summary>
        /// Returns the name.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Executes a mutation operation.
        /// </summary>
        /// <param name="mutating"></param>
        /// <returns></returns>
        Individual<GenomeType, ProblemType, WeightType> Mutate(
            Solver<GenomeType, ProblemType, WeightType> solver,
            Individual<GenomeType, ProblemType, WeightType> mutating);
    }
}