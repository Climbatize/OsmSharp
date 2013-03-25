﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OsmSharp.Routing.Graph;
using OsmSharp.Routing.Osm.Data;
using System.IO;
using OsmSharp.Routing.Router;
using OsmSharp.Routing.Interpreter;
using OsmSharp.Tools.Math.Geo;
using OsmSharp.Osm;
using OsmSharp.Routing.Osm.Data.Processing;
using OsmSharp.Osm.Data.Core.Processor;
using OsmSharp.Osm.Data.PBF.Raw.Processor;
using OsmSharp.Routing;
using OsmSharp.Routing.Graph.Router;
using OsmSharp.Routing.Graph.Router.Dykstra;
using OsmSharp.Routing.Graph.DynamicGraph.PreProcessed;
using OsmSharp.Osm.Data.XML.Processor;

namespace OsmSharp.Routing.Osm.Test.Instructions
{
    class Point2PointDykstraBinairyHeapTests : InstructionTests<PreProcessedEdge>
    {
        public override IBasicRouterDataSource<PreProcessedEdge> BuildData(Stream data_stream, bool pbf,
            IRoutingInterpreter interpreter, GeoCoordinateBox box)
        {
            OsmTagsIndex tags_index = new OsmTagsIndex();

            // do the data processing.
            DynamicGraphRouterDataSource<PreProcessedEdge> osm_data =
                new DynamicGraphRouterDataSource<PreProcessedEdge>(tags_index);
            PreProcessedDataGraphProcessingTarget target_data = new PreProcessedDataGraphProcessingTarget(
                osm_data, interpreter, osm_data.TagsIndex, VehicleEnum.Car, box);
            DataProcessorSource data_processor_source;
            if (pbf)
            {
                data_processor_source = new PBFDataProcessorSource(data_stream);
            }
            else
            {
                data_processor_source = new XmlDataProcessorSource(data_stream);
            }

            //DataProcessorFilterSort sorter = new DataProcessorFilterSort();
            //sorter.RegisterSource(data_processor_source);
            //data_processor_source =
            //    new OsmSharp.Osm.Data.Core.Processor.Progress.ProgressDataProcessorSource(
            //        data_processor_source);
            target_data.RegisterSource(data_processor_source);
            target_data.Pull();

            return osm_data;
        }

        public override IRouter<RouterPoint> BuildRouter(IBasicRouterDataSource<PreProcessedEdge> data,
            IRoutingInterpreter interpreter, IBasicRouter<PreProcessedEdge> router_basic)
        {
            return new Router<PreProcessedEdge>(data, interpreter, router_basic);
        }

        public override IBasicRouter<PreProcessedEdge> BuildBasicRouter(IBasicRouterDataSource<PreProcessedEdge> data)
        {
            return new DykstraRoutingPreProcessed(data.TagsIndex);
        }
    }
}
